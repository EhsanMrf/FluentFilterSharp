using System.Linq.Expressions;
using FilterSharp.Attribute;
using FilterSharp.Dto;
using FilterSharp.Filter.Operator.ContractFilter;
using FilterSharp.Input;
using FilterSharp.StaticNames;

namespace FilterSharp.Filter.Operator;

[OperatorName(FilterOperatorNames.InRange)]
internal class InRangeFilter :IFilterStrategy
{
    public  Expression Apply(FilterContext context)
    {
        var greaterThanOrEqual = Expression.GreaterThanOrEqual(context.Property, context.Constant![0]);
        var lessThanOrEqual = Expression.LessThanOrEqual(context.Property, context.Constant![1]);

        return Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
    }
}