using FilterSharp.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilterSharp.FluentSharp;

public class FilterSharpMapperDto
{
    public bool CanFilter { get; private set; } = true;
    public bool CanSort { get; private set; } = true;
    public string? FilterFieldName { get; private set; }
    public HashSet<FilterOperator>? CanOperatorNames { get; set; }
    
    public FilterSharpMapperDto(bool canFilter, bool canSort, string? filterFieldName, HashSet<FilterOperator>? canOperatorNames)
    {
        CanFilter = canFilter;
        CanSort = canSort;
        FilterFieldName = filterFieldName;
        CanOperatorNames = canOperatorNames;
    }

    private FilterSharpMapperDto()
    {
        
    }
}