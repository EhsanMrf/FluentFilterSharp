using FilterSharp.Extensions;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.ProcessorRequest.ProcessFields.Filter;

public class FilterRequestFieldsChange :IRequestFieldsChange,IScopeService
{
    
    public void ChangeFields(DataQueryRequest queryRequest, ICollection<FilterSharpMapper>? sharpMappers)
    {
        var filters = queryRequest.Filters?.ToList();

        if (filters is {Count: 0} or null) return;

        foreach (var filterSharpMapper in sharpMappers!)
        {
            List<string>? filterOperators = null;
            if (filterSharpMapper.AllowedOperators is {Count: > 0})
            {
                filterOperators = [];
                foreach (var filterOperator in filterSharpMapper.AllowedOperators)
                {
                    var displayName = filterOperator.GetEnumDisplayName();
                    if (displayName != null)
                        filterOperators.Add(displayName);
                }
            }

            foreach (var filterRequest in filters!)
            {
                
                if (filterRequest.Field.Equals(filterSharpMapper.GetField()) 
                    && !filterRequest.Field.Equals(filterSharpMapper.FilterFieldName!))
                    throw new InvalidOperationException($"Filtering not allowed");
                
                if (filterRequest.Field.Equals(filterSharpMapper.FilterFieldName))
                {
                    filterRequest.Field = filterSharpMapper.GetField();
                    if (!filterSharpMapper.CanFilter)
                        throw new InvalidOperationException($"Filtering not allowed");
                }
                
                GuardOnFilterOperators( filterOperators, filterRequest);
            }
        }
    }

    private void GuardOnFilterOperators(List<string>? filterOperators,FilterRequest filterRequest)
    {
        if (filterOperators is null) return;
        
        var isOperatorAllowed = filterOperators.Any(a => a == filterRequest.Operator);
        if (!isOperatorAllowed) 
            throw new InvalidOperationException("Filtering not allowed Operator");
    }

}