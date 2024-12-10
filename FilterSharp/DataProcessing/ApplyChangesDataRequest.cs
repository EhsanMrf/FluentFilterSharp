using FilterSharp.Caching;
using FilterSharp.DataProcessing.Mapp;
using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing;

public sealed class ApplyChangesDataRequest : IApplyChangesDataRequest
{
    private readonly IMapperCacheManager _mapperCacheManager =null!;
    private readonly IMapperConfigurator _mapperConfigurator=null!;
    private readonly IDataRequestProcessor _dataProcessor=null!;

    private ApplyChangesDataRequest()
    {

    }
    public ApplyChangesDataRequest(IMapperCacheManager mapperCacheManager, IMapperConfigurator mapperConfigurator, IDataRequestProcessor dataProcessor)
    {
        _mapperCacheManager = mapperCacheManager;
        _mapperConfigurator = mapperConfigurator;
        _dataProcessor = dataProcessor;
    }

    public void GetDataChange<T>(DataQueryRequest queryRequest) where T : class
    {
        var filterSharpMapper = _mapperCacheManager.GetMapper<T>();
        if (filterSharpMapper == null)
            return;

        var builder = _mapperConfigurator.Configure(filterSharpMapper);
        _dataProcessor.ApplyChanges(queryRequest, builder);
    }
}

public interface IApplyChangesDataRequest
{
    void GetDataChange<T>(DataQueryRequest queryRequest) where T : class;
}