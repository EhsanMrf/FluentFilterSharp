namespace FilterSharp.Input;

public sealed class Filter
{
    /// <summary>
    /// field = column database
    /// </summary>
    public string Field { get; set; }
    /// <summary>
    /// default : and 
    /// </summary>
    public string Logic { get; set; }

    /// <summary>
    /// value :  automatic convert to type
    /// </summary>
    public string Value { get; set; }
    /// <summary>
    /// operator : (equals,notEqual,lessThan,greaterThan,greaterThanOrEqual,startsWith,endsWith,contains,notContains,blank,notBlank,inRange)
    /// </summary>
    public string Operator { get; set; }

    private Filter() { }
    public Filter(string? field, string logic, string? value)
    {
        Field = field;
        Logic = logic;
        Value = value;
    }
    
    
}