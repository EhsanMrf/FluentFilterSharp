using FilterSharp.DataProcessing.ProcessorRequest;
using FilterSharp.FluentSharp;
using FilterSharp.Input;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharp.DataProcessing.ChangeRequest;

public sealed class ApplyChangesDataRequest : IApplyChangesDataRequest
{
    private readonly IServiceProvider _serviceProvider = null!;

    private ApplyChangesDataRequest()
    {
    }

    public ApplyChangesDataRequest(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public void GetDataChange<T>(DataQueryRequest? queryRequest) where T : class
    {
        if (queryRequest == null) return;

        var mapper = _serviceProvider.GetService(typeof(AbstractFilterSharpMapper<T>)) as AbstractFilterSharpMapper<T>;

        var attributeBasedMapperProvider = _serviceProvider.GetRequiredService<IAttributeBasedMapperProvider>();
        var filterSharpMappers = attributeBasedMapperProvider.GetListSharpMapper(mapper);
        if (filterSharpMappers == null)
            return;

        var dataRequestProcessor = _serviceProvider.GetRequiredService<IDataRequestProcessor>();
        dataRequestProcessor.ApplyChanges(queryRequest, filterSharpMappers!.ToList());
    }
}