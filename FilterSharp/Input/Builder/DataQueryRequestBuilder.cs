namespace FilterSharp.Input.Builder;

public sealed class DataQueryRequestBuilder
{
    private List<FilterRequest>? _filters = new();
    private HashSet<SortingRequest>? _sorting = null;
    private HashSet<string>? _selects = null;
    private int? _pageNumber = null;
    private int? _pageSize = null;

    public DataQueryRequestBuilder AddFilter(string field, string @operator, string value)
    {
        _filters.Add(FilterRequest.Create(field, @operator, value));
        return this;
    }
    
    public DataQueryRequestBuilder AddFilter(FilterRequest filterRequest)
    {
        _filters.Add(filterRequest);
        return this;
    } 
    
    public DataQueryRequestBuilder AddSelects(HashSet<string> selects)
    {
        _selects = selects;
        return this;
    }

    public DataQueryRequestBuilder AddSorting(string fieldName, bool ascending)
    {
        _sorting ??= new HashSet<SortingRequest>();
        _sorting.Add(new SortingRequest(fieldName, ascending));
        return this;
    }

    public DataQueryRequestBuilder SetPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public DataQueryRequestBuilder SetPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public DataQueryRequest Build()
    {
        return new DataQueryRequest
        {
            Filters = _filters,
            Sorting = _sorting,
            PageNumber = _pageNumber ?? 0,
            PageSize = _pageSize ?? 0,
            Selects = _selects
        };
    }
}