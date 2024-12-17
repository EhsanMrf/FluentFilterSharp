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
}