using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.StartsWith)]
internal class StartsWithFilter:IFilterStrategy
{
    public Expression Apply(FilterContext context)
    {
        var startsWithMethod = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
        return Expression.Call(context.Property, startsWithMethod!, context.Constant);
    }
}