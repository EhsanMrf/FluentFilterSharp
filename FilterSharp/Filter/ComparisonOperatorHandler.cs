using System.Linq.Expressions;
using FilterSharp.Input;
using System.Reflection.Metadata;
using FilterSharp.StaticNames;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

internal static class MemberExpressionHandler
{
    internal static MemberExpression GetMemberExpression(FilterRequest filterRequest, ParameterExpression parameter)
    {
        var propertyNames = filterRequest.Field.Split('.');

        if (propertyNames.Length==1)
            return Expression.Property(parameter, filterRequest.Field);

        Expression currentExpression = parameter;
        foreach (var propertyName in propertyNames)
            currentExpression = Expression.Property(currentExpression, propertyName);
        

        return (MemberExpression)currentExpression;
    }
}