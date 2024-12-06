using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.Input;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.Contains)]
internal class ContainsFilter :IFilterStrategy
{
    public  Expression Apply(FilterContext context)
    {
        var containsMethod = typeof(string).GetMethod(FilterOperatorNames.ContainsToUpper, new[] { typeof(string) });
        return Expression.Call(context.Property, containsMethod!, context.Constant);
    }
}