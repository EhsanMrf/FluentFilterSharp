using System.Linq.Expressions;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter;

internal static class LogicalOperatorHandler
{
    private static readonly IDictionary<string, Func<Expression, Expression, Expression>> LogicalOperators =
        new Dictionary<string, Func<Expression, Expression, Expression>>(StringComparer.OrdinalIgnoreCase)
        {
            { FilterLogicalNames.LogicAnd, Expression.AndAlso },
            { FilterLogicalNames.LogicOr, Expression.OrElse }
        };

    internal static Func<Expression, Expression, Expression> GetOperator(string operatorName)
    {
        if (LogicalOperators.TryGetValue(operatorName, out var operatorFunc))
            return operatorFunc;

        throw new InvalidOperationException($"Unknown logical operator: {operatorName}");
    }
}