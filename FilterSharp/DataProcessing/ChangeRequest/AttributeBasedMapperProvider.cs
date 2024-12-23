using FilterSharp.Attribute;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.Enum;
using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.ChangeRequest;

public sealed class AttributeBasedMapperProvider(IMapperConfigurator mapperConfigurator) :IAttributeBasedMapperProvider,ISingletonService
{
    public IEnumerable<FilterSharpMapper>? GetListSharpMapper<T>(AbstractFilterSharpMapper<T>? sharpMapper) where T : class
    { 
        return GetListBySharpMappers(sharpMapper) ?? GetListByAttribute<T>();
    }
    
    private IEnumerable<FilterSharpMapper>? GetListBySharpMappers<T>(AbstractFilterSharpMapper<T>? sharpMapper)
        where T : class
    {
        if (sharpMapper == null)
            return null;

        var builder = mapperConfigurator.Configure(sharpMapper);
        return builder.GetSharpMappers();
    }

    private IEnumerable<FilterSharpMapper>? GetListByAttribute<T>()
    {
        var properties = typeof(T).GetProperties();
        var result = new List<FilterSharpMapper>();

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttributes(typeof(FilterSharpAttribute), false)
                .FirstOrDefault() as FilterSharpAttribute;

            if (attribute != null)
            {
                var mapper = new FilterSharpMapper(property.Name);
                mapper.SetData(attribute.CanSort, attribute.CanSort, attribute.FilterFieldName??property.Name,
                    [..attribute.AllowedOperators]);
                result.Add(mapper);
            }
        }

        return result.Count != 0 ? result : null;
    }
}