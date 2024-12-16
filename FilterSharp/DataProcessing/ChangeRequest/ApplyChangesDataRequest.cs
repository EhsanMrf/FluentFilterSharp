using FilterSharp.Attribute;
using FilterSharp.Caching;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing;

public sealed class ApplyChangesDataRequest : IApplyChangesDataRequest
{
    private readonly IMapperCacheManager _mapperCacheManager = null!;
    private readonly IMapperConfigurator _mapperConfigurator = null!;
    private readonly IDataRequestProcessor _dataProcessor = null!;

    private ApplyChangesDataRequest()
    {
    }

    public ApplyChangesDataRequest(IMapperCacheManager mapperCacheManager, IMapperConfigurator mapperConfigurator,
        IDataRequestProcessor dataProcessor)
    {
        _mapperCacheManager = mapperCacheManager;
        _mapperConfigurator = mapperConfigurator;
        _dataProcessor = dataProcessor;
    }

    public void GetDataChange<T>(DataQueryRequest? queryRequest) where T : class
    {
        if (queryRequest == null) return;

        var filterSharpMapper = _mapperCacheManager.GetMapper<T>();
        if (filterSharpMapper == null)
            return;

        var builder = _mapperConfigurator.Configure(filterSharpMapper);

        _dataProcessor.ApplyChanges(queryRequest, null);
    }
}

public sealed class ApplyChangesDataRequestHandler(IMapperConfigurator mapperConfigurator)
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
                mapper.SetData(attribute.IsDisableSort, attribute.IsDisableSort, attribute.FilterFieldName,
                    attribute.AllowedOperators);
                result.Add(mapper);
            }
        }

        return result.Count != 0 ? result : null;
    }
}