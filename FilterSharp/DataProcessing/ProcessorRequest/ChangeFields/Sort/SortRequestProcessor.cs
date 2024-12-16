using FilterSharp.DataProcessing.ProcessorRequest.ChangeFields;
using FilterSharp.Exceptions;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest.Sort;

public class SortRequestProcessor :IRequestFieldsChange
{
    public void ChangeFields(DataQueryRequest queryRequest, ICollection<FilterSharpMapper>? sharpMappers)
    {
        var queryRequestFilters = queryRequest.Sorting?.ToList();

        if (queryRequestFilters ==null || queryRequestFilters.Count==0) return;
        var sortingRequests = new HashSet<SortingRequest>();
        foreach (var sort in queryRequest!.Sorting!)
        {
            var mapper = sharpMappers!.FirstOrDefault(x => x.GetField().Equals(sort.FiledName));
            GuardOnSharpMapper(mapper,sort);

            sortingRequests.Add(new SortingRequest(mapper != null ? mapper.FilterFieldName! : sort.FiledName,
                sort.Ascending));
        }

        queryRequest.Sorting = sortingRequests;
    }

    private void GuardOnSharpMapper(FilterSharpMapper? mapper,SortingRequest sortingRequest)
    {
        if (mapper is null)
            return;
        
        if (mapper!.FilterFieldName != sortingRequest.FiledName)
            throw new SortRequestProcessorException("You are not allowed to perform sort");
        
        if (!mapper!.CanSort)
            throw new SortRequestProcessorException("Sorting is not allowed for this field.");
    }
}