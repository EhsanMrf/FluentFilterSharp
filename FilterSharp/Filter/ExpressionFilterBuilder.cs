using System.Linq.Expressions;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.Register;
using FilterSharp.Input;

namespace FilterSharp.Filter;

internal static class ExpressionFilterBuilder<T>
{

    internal static Expression<Func<T, bool>> Build(IList<Input.FilterRequest> filters)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var expression = BuildExpression(filters, parameter);
        return Expression.Lambda<Func<T, bool>>(expression, parameter);
    }

    private static Expression BuildExpression(IEnumerable<Input.FilterRequest> filters, ParameterExpression parameter)
    {
        Expression? result = null;

        foreach (var filter in filters)
        {
            var filterExpression = BuildFilterExpression(filter, parameter);

            result = result == null
                ? filterExpression
                : LogicalOperatorHandler.GetOperator(filter.Logic)(result, filterExpression);
        }

        return result!;
    }
    
    private static Expression BuildFilterExpression(Input.FilterRequest filterRequest, ParameterExpression parameter)
    {
        var property = Expression.Property(parameter, filterRequest.Field);
        var value = ValueConverter.ConvertValue(filterRequest.Value, property.Type);
        var constant = Expression.Constant(value, property.Type);

        var strategy = FilterStrategyRegistry.GetStrategy(filterRequest.Operator!);
        if (strategy != null)
            return strategy.Apply(new FilterContext(property,constant: constant,filterRequest));
        
        var comparisonOperator = ComparisonOperatorHandler.GetOperator(filterRequest.Operator!);
        return comparisonOperator(property, constant);
    }
}