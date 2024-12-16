using FilterSharp.DataProcessing.Dto;
using FilterSharp.DataProcessing.ProcessorRequest.Filter;
using FilterSharp.DataProcessing.ProcessorRequest.Sort;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public class RequestLogicProcessor(IFilterRequestProcessor filterRequestProcessor, ISortRequestProcessor sortRequestProcessor) : IDataRequestProcessor
{
    public void ApplyChanges<T>(FilterQueryContext<T> context) where T : class
    {
        var filterSharpMappers = context.Builder?.GetSharpMappers().ToList();
        if (filterSharpMappers?.Count == 0) return;
        
        filterRequestProcessor.FilterFieldsChange<T>(context.DataQueryRequest!, filterSharpMappers);
        sortRequestProcessor.SortFieldChange<T>(context.DataQueryRequest!, filterSharpMappers);
    }
}

public class RequestAttributeProcessor:IDataRequestProcessor
{
    public void ApplyChanges<T>(FilterQueryContext<T> context) where T : class
    {
        throw new NotImplementedException();
    }
}