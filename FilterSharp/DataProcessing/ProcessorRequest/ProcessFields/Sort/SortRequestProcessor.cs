using FilterSharp.Exceptions;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Sort;

public class SortRequestProcessor :IRequestFieldsChange,IScopeService
{
    public void ChangeFields(DataQueryRequest queryRequest, ICollection<FilterSharpMapper>? sharpMappers)
    {
        var queryRequestFilters = queryRequest.Sorting?.ToList();

        if (queryRequestFilters ==null || queryRequestFilters.Count==0) return;
        var sortingRequests = new HashSet<SortingRequest>();
        foreach (var sort in queryRequest!.Sorting!)
        {
            var mapper = GetSharpMapper(sort, sharpMappers);
            GuardOnSharpMapper(mapper,sort);

            sortingRequests.Add(new SortingRequest(mapper != null ? mapper.GetField()! : sort.FiledName,
                sort.Ascending));
        }

        queryRequest.SetSorting(sortingRequests);
    }

    private FilterSharpMapper? GetSharpMapper(SortingRequest sortingRequest,ICollection<FilterSharpMapper>? sharpMappers)
    {
        return sharpMappers!
            .FirstOrDefault(x =>x.GetField()!=null && x.GetField()!.Equals(sortingRequest.FiledName) 
                                || x.FilterFieldName!=null&& x.FilterFieldName!.Equals(sortingRequest.FiledName));
    }
    private void GuardOnSharpMapper(FilterSharpMapper? mapper,SortingRequest sortingRequest)
    {
        if (mapper is null)
            return;
        
        if (sortingRequest.FiledName.Equals(mapper.GetField()) 
            && !sortingRequest.FiledName.Contains(mapper.FilterFieldName!))
            throw new InvalidOperationException($"Sort not allowed");
        
        if (mapper!.FilterFieldName != sortingRequest.FiledName)
            throw new SortRequestProcessorException("You are not allowed to perform sort");
        
        if (!mapper!.CanSort)
            throw new SortRequestProcessorException("Sorting is not allowed for this field.");
    }
}