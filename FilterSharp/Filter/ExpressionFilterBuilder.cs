using System.Linq.Expressions;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.Register;
using FilterSharp.Input;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter;

internal static class ExpressionFilterBuilder<T>
{
    internal static Expression<Func<T, bool>> Build(IList<Input.FilterRequest> filters)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var expression = BuildExpression(filters, parameter);
        return Expression.Lambda<Func<T, bool>>(expression, parameter);
    }

    private static Expression BuildExpression(IEnumerable<FilterRequest> filters, ParameterExpression parameter)
    {
        Expression? result = null;

        var filterLogicalName = FilterLogicalNames.LogicAnd;
        foreach (var filter in filters)
        {
            var filterExpression = BuildFilterExpression(filter, parameter);

            result = result == null
                ? filterExpression
                : LogicalOperatorHandler.GetOperator(filterLogicalName!)(result, filterExpression);
            
            filterLogicalName = filter.Logic;
        }

        return result!;
    }

    private static Expression BuildFilterExpression(FilterRequest filterRequest, ParameterExpression parameter)
    {
        var property = Expression.Property(parameter, filterRequest.Field);
        var value = ValueConverter.ConvertValue(filterRequest.Value, property.Type);
        var constant = Expression.Constant(value, property.Type);

        var strategy = FilterStrategyRegistry.GetStrategy(filterRequest.Operator!);
        if (strategy != null)
            return strategy.Apply(new FilterContext(property, constant: constant, filterRequest));

        var comparisonOperator = ComparisonOperatorHandler.GetOperator(filterRequest.Operator!);
        return comparisonOperator(property, constant);
    }
}