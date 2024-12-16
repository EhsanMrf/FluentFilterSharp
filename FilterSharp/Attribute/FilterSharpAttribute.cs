using FilterSharp.Enum;

namespace FilterSharp.Attribute;

public sealed class FilterSharpAttribute : System.Attribute
{
    public string? FilterFieldName { get; set; }
    /// <summary>
    /// Default true
    /// </summary>
    public bool CanSort { get; set; } = true;
    /// <summary>
    /// Default true
    /// </summary>
    public bool CanFilter { get; set; } = true;
    public HashSet<FilterOperator>? AllowedOperators { get; set; }

    public FilterSharpAttribute()
    {
    }

    public FilterSharpAttribute(string? filterFieldName, bool canSort, bool canFilter,
        HashSet<FilterOperator>? allowedOperators)
    {
        FilterFieldName = filterFieldName;
        CanSort = canSort;
        CanFilter = canFilter;
        AllowedOperators = allowedOperators;
    }

    public FilterSharpAttribute(string? filterFieldName, bool canSort = false, bool canFilter = false)
    {
        FilterFieldName = filterFieldName;
        CanSort = canSort;
        CanFilter = canFilter;
    }

    public FilterSharpAttribute(string? filterFieldName)
    {
        FilterFieldName = filterFieldName;
    }

    public FilterSharpAttribute(HashSet<FilterOperator>? allowedOperators)
    {
        AllowedOperators = allowedOperators;
    }
}