using FilterSharp.Caching;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing;

public class ApplyChangesDataRequest : IApplyChangesDataRequest
{
    private readonly IMapperCacheManager _mapperCacheManager;
    private readonly IMapperConfigurator _mapperConfigurator;
    private readonly IDataRequestProcessor _dataProcessor;

    private ApplyChangesDataRequest()
    {

    }
    public ApplyChangesDataRequest(IMapperCacheManager mapperCacheManager, IMapperConfigurator mapperConfigurator, IDataRequestProcessor dataProcessor)
    {
        _mapperCacheManager = mapperCacheManager;
        _mapperConfigurator = mapperConfigurator;
        _dataProcessor = dataProcessor;
    }

    public void GetDataChange<T>(DataRequest request) where T : class
    {
        var filterSharpMapper = _mapperCacheManager.GetMapper<T>();
        if (filterSharpMapper == null)
            return;

        var builder = _mapperConfigurator.Configure(filterSharpMapper);
        _dataProcessor.ApplyChanges(request, builder);
    }
}

public interface IApplyChangesDataRequest
{
    void GetDataChange<T>(DataRequest request) where T : class;
}