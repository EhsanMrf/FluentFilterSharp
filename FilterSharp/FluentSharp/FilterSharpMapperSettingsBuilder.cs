using FilterSharp.Enum;
using FilterSharp.FluentSharp.Model;

namespace FilterSharp.FluentSharp;

public sealed class FilterSharpMapperSettingsBuilder
{
    internal FilterSharpMapper FilterSharpMapper { get; init; } = null!;
    public FilterSharpMapperSettingsBuilder FilterFieldName(string filterFieldName)
    {
        FilterSharpMapper.FilterFieldName = filterFieldName;
        return this; 
    }

    /// <summary>
    /// default filter enable (true)
    /// </summary>
    public FilterSharpMapperSettingsBuilder DisableFilter()
    {
        FilterSharpMapper.CanFilter = false;
        return this;
    }
    /// <summary>
    /// default sort enable (true)
    /// /// </summary>
    public FilterSharpMapperSettingsBuilder DisableSort()
    {
        FilterSharpMapper.CanSort = false;
        return this;
    }

    public FilterSharpMapperSettingsBuilder AllowedOperators(FilterOperator[] filterOperators)
    {
        FilterSharpMapper.AllowedOperators= [..filterOperators];
        return this;
    }
}