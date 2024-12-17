using System.Text.Json.Serialization;

namespace FilterSharp.Input;

public sealed class SortingRequest
{
    
    [property: JsonPropertyName("filedName")]
    public string FiledName { get; } = null!;
    public bool Ascending { get; }

    private SortingRequest()
    {
        
    }
    [JsonConstructor]
    public SortingRequest(string filedName, bool ascending)
    {
        FiledName = filedName;
        Ascending = ascending;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is SortingRequest other)
        {
            return FiledName == other.FiledName && Ascending == other.Ascending;
        }
        return false;    
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StringComparer.Ordinal.GetHashCode(FiledName), Ascending);
    }
}