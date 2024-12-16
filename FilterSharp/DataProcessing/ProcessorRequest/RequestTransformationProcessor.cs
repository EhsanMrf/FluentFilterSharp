using FilterSharp.DataProcessing.ProcessorRequest.ChangeFields;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;
using Microsoft.Extensions.DependencyInjection;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public class RequestTransformationProcessor(IServiceProvider serviceProvider) : IDataRequestProcessor
{
    public void ApplyChanges(DataQueryRequest dataQueryRequest,ICollection<FilterSharpMapper>? sharpMappers)
    {
        if (sharpMappers?.Count == 0) return;

        var requiredService = serviceProvider.GetRequiredService<IEnumerable<IRequestFieldsChange>>();
        foreach (var requestFieldsChange in requiredService)
            requestFieldsChange.ChangeFields(dataQueryRequest!, sharpMappers);
    }
} 