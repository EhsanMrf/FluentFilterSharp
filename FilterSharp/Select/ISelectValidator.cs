namespace FilterSharp.Select;

public interface ISelectValidator 
{
    void Validate<T>(HashSet<string> selects) where T : class;
}