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
        var rangeValues = context.FilterRequest.Value?.Split(",");
        if (rangeValues?.Length != 2)
            throw new ArgumentException("Value for inRange must contain exactly two values separated by a comma.");

        var lowerBound = Expression.Constant(Convert.ChangeType(rangeValues[0], context.Property.Type));
        var upperBound = Expression.Constant(Convert.ChangeType(rangeValues[1], context.Property.Type));

        var greaterThanOrEqual = Expression.GreaterThanOrEqual(context.Property, lowerBound);
        var lessThanOrEqual = Expression.LessThanOrEqual(context.Property, upperBound);

        return Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);
    }

    public Expression Apply(MemberExpression property)=>throw new NotImplementedException("This strategy does not support a value parameter.");
}