using FilterSharp.TransActionService;

namespace FilterSharp.Select;

public class SelectValidator :ISelectValidator,IScopeService
{
    public void Validate<T>(HashSet<string> selects) where T : class
    {
        var propertyNames = typeof(T).GetProperties()
            .Select(p => p.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        
        var invalidSelects = selects.Where(select => !propertyNames.Contains(select)).ToList();
        if (invalidSelects.Count != 0)
        {
            throw new InvalidOperationException(
                $"The following properties do not exist in {typeof(T).Name}: {string.Join(", ", invalidSelects)}");
        }
    }
}