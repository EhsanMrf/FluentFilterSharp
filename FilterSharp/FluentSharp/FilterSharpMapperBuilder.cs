using System.Linq.Expressions;
using FilterSharp.Extensions;
using FilterSharp.FluentSharp.Model;

namespace FilterSharp.FluentSharp;

public sealed class FilterSharpMapperBuilder<T>
{ 
    private readonly HashSet<FilterSharpMapper> _sharpMappers;

    public FilterSharpMapperBuilder()
    {
        _sharpMappers = new HashSet<FilterSharpMapper>();
    }

    /// <summary>
    /// </summary>
    /// <param name="fieldSelector"></param>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public FilterSharpMapperSettingsBuilder OnField<TProperty>(Expression<Func<T, TProperty>> fieldSelector)
    {
        fieldSelector.Guard("Cannot pass null to OnField", nameof(fieldSelector));

        if (fieldSelector.Body is not MemberExpression memberExpression)
            throw new ArgumentException("Expression must be a MemberExpression", nameof(fieldSelector));
        
        var fieldName = memberExpression.Member.Name;
        var mapper = new FilterSharpMapper(fieldName);
        mapper.FilterFieldName ??= fieldName;
        var builder = new FilterSharpMapperSettingsBuilder { FilterSharpMapper = mapper };

        _sharpMappers.Add(builder.FilterSharpMapper);
        return builder;
    } 
    
    public void AllowedSelects(HashSet<string> selects)
    {
        selects.Guard("Cannot pass null to OnField", nameof(selects));

        var filterSharpMapper = new FilterSharpMapper();
        filterSharpMapper.SetSelects(selects);
        _sharpMappers.Add(filterSharpMapper);
    }
    
    
    internal IEnumerable<FilterSharpMapper> GetSharpMappers() => _sharpMappers;

    /// <summary>
    /// Configures a field for filtering and sorting in the FilterSharpMapperBuilder.
    /// This method allows applying custom configurations to a field, such as setting its filter name, enabling filtering and sorting, etc.
    /// </summary>
    /// <param name="fieldSelector">
    /// A lambda expression that selects the field of the entity to be configured. 
    /// This is used to extract the field name from the entity.
    /// </param>
    /// <param name="configure">
    /// An action delegate that allows applying custom configurations to the <see cref="FilterSharpMapper"/> instance.
    /// You can modify properties like <see cref="FilterSharpMapper.FilterFieldName"/>, enable filtering or sorting, etc.
    /// </param>
    /// <param name="sharpMapperOnly">
    /// A boolean flag indicating whether the field should only be included in the mapper for filtering and sorting, 
    /// without being processed further. The default value is <c>false</c>.
    /// </param>
    /// <typeparam name="TProperty">
    /// The type of the property being configured in the entity.
    /// </typeparam>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="fieldSelector"/> does not represent a valid member expression 
    /// (i.e., it is not a <see cref="MemberExpression"/>).
    /// </exception>
    public void OnField<TProperty>(Expression<Func<T, TProperty>> fieldSelector,
        Action<FilterSharpMapper> configure, bool sharpMapperOnly = false)
    {
        fieldSelector.Guard("Cannot pass null to OnField", nameof(fieldSelector));

        if (fieldSelector.Body is MemberExpression memberExpression)
        {
            var fieldName = memberExpression.Member.Name;
            var mapper = new FilterSharpMapper(fieldName);
            mapper.FilterFieldName ??= fieldName;
            configure(mapper);
            _sharpMappers.Add(mapper);
        }
        else
            throw new ArgumentException("Expression must be a MemberExpression", nameof(fieldSelector));
    }  
    
}