using System.Linq.Expressions;

namespace FilterSharp.Input;

public class FilterContext
{
    public MemberExpression Property { get; set; } = null!;
    public ConstantExpression? Constant { get; set; }
    public Filter Filter { get; set; }

    public FilterContext(MemberExpression property, ConstantExpression? constant, Filter filter)
    {
        Property = property;
        Constant = constant;
        Filter = filter;
    }
}