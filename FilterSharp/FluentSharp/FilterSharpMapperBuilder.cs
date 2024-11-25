using System.Linq.Expressions;
using FilterSharp.Extensions;
using FilterSharp.FluentSharp.Model;

namespace FilterSharp.FluentSharp;

public class FilterSharpMapperBuilder<T>
{
    private string Field { get; set; } = null!;
    private bool CanFilter { get; set; }
    private bool CanSort { get; set; }
    private string? FilterFieldName { get; set; }
    private IEnumerable<string>? CanOperatorNames { get; set; }

    private readonly HashSet<FilterSharpMapper> SharpMappers;

    public FilterSharpMapperBuilder()
    {
        SharpMappers = new HashSet<FilterSharpMapper>();
    }

    internal IEnumerable<FilterSharpMapper> GetSharpMappers()=>SharpMappers;

    public virtual void OnField<TProperty>(Expression<Func<T, TProperty>> fieldSelector, Action<FilterSharpMapper> configure)
    {
        fieldSelector.Guard("Cannot pass null to OnField", nameof(fieldSelector));

        if (fieldSelector.Body is MemberExpression memberExpression)
        {
            var fieldName = memberExpression.Member.Name;
            var mapper = new FilterSharpMapper (fieldName);
            configure(mapper);
            SharpMappers.Add(mapper);
        }
        else
            throw new ArgumentException("Expression must be a MemberExpression", nameof(fieldSelector));
    }
}