using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.Input;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.NotContains)]
internal class NotContainsFilter :IFilterStrategy
{
    public  Expression Apply(FilterContext context)
    {
        var containsMethod = typeof(string).GetMethod(FilterOperatorNames.ContainsToUpper, new[] { typeof(string) });
        var containsExpression = Expression.Call(context.Property, containsMethod!, context.Constant);
        return Expression.Not(containsExpression);
    }

    /// <summary>
    /// not suport
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Expression Apply(MemberExpression property)=>throw new NotImplementedException("This strategy does not support a value parameter.");
    
}