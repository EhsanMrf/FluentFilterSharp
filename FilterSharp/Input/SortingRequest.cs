namespace FilterSharp.Input;

public sealed class SortingRequest
{
    public string FiledName { get; set; } = null!;
    public bool Ascending { get;  set; }

    private SortingRequest()
    {
        
    }
    public SortingRequest(string filedName, bool ascending)
    {
        FiledName = filedName;
        Ascending = ascending;
    }
}