namespace FilterSharp.Input;

public sealed class SortingRequest
{
    internal string FiledName { get; } = null!;
    internal bool Ascending { get; }

    private SortingRequest()
    {
        
    }
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