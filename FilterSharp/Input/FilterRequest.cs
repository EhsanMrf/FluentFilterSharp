using FilterSharp.StaticNames;

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
    public string? Logic { get; private set; } = FilterLogicalNames.LogicAnd;

    /// <summary>
    /// value :  automatic convert to type
    /// </summary>
    public string Value { get; private set; } = null!;

    /// <summary>
    /// operator : (equals,notEqual,lessThan,greaterThan,greaterThanOrEqual,startsWith,endsWith,contains,notContains,blank,notBlank,inRange)
    /// </summary>
    public string Operator { get; private set; } = null!;

    private FilterRequest() { }
    private FilterRequest(string field, string @operator, string value)
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    } 
    
    private FilterRequest(string field, string @operator, string value,string logic)
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
        Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Logic = logic;
    }

    public static FilterRequest Create(string field, string @operator, string value)
    {
        return new FilterRequest(field, @operator, value);
    }
    
    public static FilterRequest Create(string field, string @operator, string value,string logic)
    {
        return new FilterRequest(field, @operator, value,logic);
    }
    
    
}