using System.Linq.Expressions;
using FilterSharp.Input;

namespace FilterSharp.Dto;

public class FilterContext
{
    public MemberExpression Property { get; set; } = null!;
    public ConstantExpression? Constant { get; set; }
    public FilterRequest FilterRequest { get; set; }

    public FilterContext(MemberExpression property, ConstantExpression? constant, FilterRequest filterRequest)
    {
        Property = property;
        Constant = constant;
        FilterRequest = filterRequest;
    }
}