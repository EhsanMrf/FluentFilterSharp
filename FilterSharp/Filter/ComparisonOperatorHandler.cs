using System.Linq.Expressions;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter;

internal static class ComparisonOperatorHandler
{
    private static readonly IDictionary<string, Func<Expression, Expression, Expression>> ComparisonOperators =
        new Dictionary<string, Func<Expression, Expression, Expression>>(StringComparer.OrdinalIgnoreCase)
        {
            { FilterOperatorNames.Equals, Expression.Equal },
            { FilterOperatorNames.NotEquals, Expression.NotEqual },
            { FilterOperatorNames.LessThan, Expression.LessThan },
            { FilterOperatorNames.LessThanOrEqual, Expression.LessThanOrEqual },
            { FilterOperatorNames.GreaterThan, Expression.GreaterThan },
            { FilterOperatorNames.GreaterThanOrEqual, Expression.GreaterThanOrEqual },
        };

    public static Func<Expression, Expression, Expression> GetOperator(string operatorName)
    {
        if (ComparisonOperators.TryGetValue(operatorName, out var operatorFunc))
            return operatorFunc;

        throw new InvalidOperationException($"Unknown comparison operator: {operatorName}");
    }
}