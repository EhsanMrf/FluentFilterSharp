namespace FilterSharp.Input;

public sealed class DataRequest
{
    public IEnumerable<Filter> Filters { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    private DataRequest()
    {
        
    }
    public DataRequest(IEnumerable<Filter> filters)
    {
        Filters = filters;
    }
}