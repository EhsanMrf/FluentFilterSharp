using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.Input;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.NotBlank)]
internal class NotBlankFilter :IFilterStrategy
{
    public  Expression Apply(FilterContext context)
    {
        var isNull = Expression.Equal(context.Property, Expression.Constant(null));
        var isEmpty = Expression.Equal(context.Property, Expression.Constant(string.Empty));
        var isBlank = Expression.OrElse(isNull, isEmpty);
        return Expression.Not(isBlank);
    }
}