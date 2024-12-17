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
    public FilterOperator[] AllowedOperators { get; set; }

    public FilterSharpAttribute()
    {
    }

}