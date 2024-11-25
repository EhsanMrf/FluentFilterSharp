using System.Linq.Expressions;
using FilterSharp.Filter.Operator.Register;
using FilterSharp.Input;

namespace FilterSharp.Filter;

internal static class ExpressionFilterBuilder<T>
{

    internal static Expression<Func<T, bool>> Build(IList<Input.Filter> filters)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var expression = BuildExpression(filters, parameter);
        return Expression.Lambda<Func<T, bool>>(expression, parameter);
    }

    private static Expression BuildExpression(IEnumerable<Input.Filter> filters, ParameterExpression parameter)
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
    
    private static Expression BuildFilterExpression(Input.Filter filter, ParameterExpression parameter)
    {
        var property = Expression.Property(parameter, filter.Field);
        var value = ValueConverter.ConvertValue(filter.Value, property.Type);
        var constant = Expression.Constant(value, property.Type);

        var strategy = FilterStrategyRegistry.GetStrategy(filter.Operator!);
        if (strategy != null)
            return strategy.Apply(new FilterContext(property,constant: constant,filter));
        
        var comparisonOperator = ComparisonOperatorHandler.GetOperator(filter.Operator!);
        return comparisonOperator(property, constant);
    }
}