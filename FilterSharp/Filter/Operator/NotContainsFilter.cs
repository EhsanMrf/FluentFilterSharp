using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.NotContains)]
internal class NotContainsFilter :IFilterStrategy
{
    public Expression Apply(FilterContext context)
    {
        var containsMethod = typeof(string).GetMethod(FilterOperatorNames.ContainsToUpper, new[] { typeof(string) });
        var containsExpression = Expression.Call(context.Property, containsMethod!, context.Constant);
        return Expression.Not(containsExpression);
    }
}