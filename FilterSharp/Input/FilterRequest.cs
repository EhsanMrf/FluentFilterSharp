using FilterSharp.StaticNames;
using System.Text.Json.Serialization;

namespace FilterSharp.Input;

public sealed class FilterRequest
{
    /// <summary>
    /// field = column database
    /// </summary>
    public string Field { get;  set; } = null!;

    /// <summary>
    /// default : and 
    /// </summary>
    public string? Logic { get;  set; } = FilterLogicalNames.LogicAnd;

    /// <summary>
    /// value :  automatic convert to type
    /// </summary>
    public string Value { get; set; } = null!;

    /// <summary>
    /// operator : (equals,notEqual,lessThan,greaterThan,greaterThanOrEqual,startsWith,endsWith,contains,notContains,blank,notBlank,inRange)
    /// </summary>
    public string Operator { get; set; } = null!;

    public IEnumerable<FilterRequest>? Filters { get; private set; }

    private FilterRequest() { }
    private FilterRequest(string field, string @operator, string value,IEnumerable<FilterRequest>? filters=null)
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonConstructor]
    private FilterRequest(string field, string @operator, string value,string logic,IEnumerable<FilterRequest>? filters=null)
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Logic = logic;
        Filters = filters;
    }

    public static FilterRequest Create(string field, string @operator, string value,IEnumerable<FilterRequest>? filters=null)
    {
        return new FilterRequest(field, @operator, value,filters);
    }
    
    public static FilterRequest Create(string field, string @operator, string value,string logic,IEnumerable<FilterRequest>? filters=null)
    {
        return new FilterRequest(field, @operator, value,logic,filters);
    }
    
    
}