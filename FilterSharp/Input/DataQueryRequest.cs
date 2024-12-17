using System.Text.Json.Serialization;

namespace FilterSharp.Input;

public  class DataQueryRequest
{
    public IEnumerable<FilterRequest>? Filters { get;  set; }
    public HashSet<SortingRequest>? Sorting { get; set; }
    
    [property: JsonPropertyName("pageNumber")]
    public int? PageNumber { get; set; }
    [property: JsonPropertyName("pageSize")]
    public int? PageSize { get; set; }

    internal void SetSorting(HashSet<SortingRequest>? sorting)
    {
        Sorting = sorting;
    }

    internal static DataQueryRequest Create (IEnumerable<FilterRequest>? filters, HashSet<SortingRequest>? sorting, int? pageNumber, int? pageSize)
    {
        return new DataQueryRequest
        {
            Filters = filters,
            Sorting = sorting,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}