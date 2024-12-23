using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Select;

public class SelectRequestFieldsChange : IRequestFieldsChange,IScopeService
{
    public void ChangeFields(DataQueryRequest queryRequest, ICollection<FilterSharpMapper>? sharpMappers)
    {
        var queryRequestSelects = queryRequest.Selects?.ToList();
        
        if (queryRequestSelects ==null || queryRequestSelects.Count==0) return;

        var selects = GetSelects(sharpMappers);
        if (selects is null) return;
        
        var invalidSelects = queryRequestSelects.Where(select => !selects.Contains(select)).ToList();
        if (invalidSelects.Any())
        {
            throw new InvalidOperationException(
                $"The following selects are not allowed: {string.Join(", ", invalidSelects)}");
        }
    }

    private HashSet<string>? GetSelects(ICollection<FilterSharpMapper>? sharpMappers)
    {
        var selects= sharpMappers?.Where(x=>x.AllowedSelects!=null).FirstOrDefault();
        return selects?.AllowedSelects;
    }
}