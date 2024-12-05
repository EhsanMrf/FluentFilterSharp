namespace FilterSharp.Input;

public sealed class DataQueryRequest
{
    public IEnumerable<FilterRequest>? Filters { get; set; }
    public IEnumerable<SortingRequest>? Sorting { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    private DataQueryRequest()
    {

    }

    public DataQueryRequest(IEnumerable<FilterRequest> filters, IEnumerable<SortingRequest>? sorting, int pageNumber, int pageSize)
    {
        Filters = filters;
        Sorting = sorting;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}