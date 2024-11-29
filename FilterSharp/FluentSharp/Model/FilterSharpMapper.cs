using FilterSharp.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FilterSharp.FluentSharp.Model;

public sealed class FilterSharpMapper
{
    private string Field { get;  set; } = null!;
    public bool CanFilter { get; set; } = true;
    public bool CanSort { get; set; } = true;
    public string? FilterFieldName { get; set; }
    public HashSet<FilterOperator>? CanOperatorNames { get; set; }

    private FilterSharpMapper()
    {
        
    }
    internal FilterSharpMapper(string field)
    {
        Field = field;
    }

    internal bool DetectedFilterOperator()
    {
        return CanOperatorNames != null && CanOperatorNames.Any();
    }

    internal string GetField() => Field;
}