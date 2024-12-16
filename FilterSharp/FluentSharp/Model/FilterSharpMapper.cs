using FilterSharp.Enum;

namespace FilterSharp.FluentSharp.Model;

public sealed class FilterSharpMapper
{
    private string Field { get; set; } = null!;
    public bool CanFilter { get; set; } = true;
    public bool CanSort { get; set; } = true;
    public string? FilterFieldName { get; set; }
    public HashSet<FilterOperator>? AllowedOperators { get; set; }

    private FilterSharpMapper()
    {
    }

    internal FilterSharpMapper(string field)
    {
        Field = field;
    }


    internal void SetData(bool canFilter, bool canSort, string? filterFieldName,
        HashSet<FilterOperator>? canOperatorNames)
    {
        CanFilter = canFilter;
        CanSort = canSort;
        FilterFieldName = filterFieldName;
        AllowedOperators = canOperatorNames;
    }

    internal string GetField() => Field;
}