namespace FilterSharp.Input;

public  class DataQueryRequest
{
    public IEnumerable<FilterRequest>? Filters { get;  set; }
    public HashSet<SortingRequest>? Sorting { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    private DataQueryRequest()
    {

    }

    public DataQueryRequest(IEnumerable<FilterRequest>? filters, HashSet<SortingRequest>? sorting, int pageNumber, int pageSize)
    {
        Filters = filters;
        Sorting = sorting;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    internal void SetSorting(HashSet<SortingRequest>? sorting)
    {
        Sorting = sorting;
    }
}