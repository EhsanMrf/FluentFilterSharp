using FilterSharp.Exceptions;
using FilterSharp.ExtendBehavior;
using FilterSharp.Extensions;
using FilterSharp.FluentSharp;
using FilterSharp.FluentSharp.Model;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public class DataRequestProcessor : IDataRequestProcessor
{
    private readonly AbstractBehaviorDataRequestProcessor? _abstractBehaviorDataRequestProcessor = null;

    public DataRequestProcessor(AbstractBehaviorDataRequestProcessor? abstractBehaviorDataRequestProcessor=null)
    {
        this._abstractBehaviorDataRequestProcessor = abstractBehaviorDataRequestProcessor;
    }

    public void ApplyChanges<T>(DataQueryRequest queryRequest, FilterSharpMapperBuilder<T> entity) where T : class
    {
        var filterSharpMappers = entity.GetSharpMappers().ToList();
        if (!filterSharpMappers.Any()) return;
        
        var filters = queryRequest.Filters?.ToList();
        GuardOnNullFilterRequest(filters);

        GuardOnSharpMapperOnly(filters,filterSharpMappers,entity);
        foreach (var filterSharpMapper in filterSharpMappers)
        {
            List<string>? filterOperators = null;
            if (filterSharpMapper.CanOperatorNames is {Count: > 0})
            {
                filterOperators = [];
                foreach (var filterOperator in filterSharpMapper.CanOperatorNames)
                {
                    var displayName = filterOperator.GetEnumDisplayName();
                    if (displayName != null)
                        filterOperators.Add(displayName);
                }
            }

            foreach (var filterRequest in filters!)
            {
                if (filterRequest.Field.Equals(filterSharpMapper.FilterFieldName))
                {
                    filterRequest.Field = filterSharpMapper.GetField();
                    if (!filterSharpMapper.CanFilter)
                        throw new InvalidOperationException($"Filtering not allowed");
                }

                if (filterOperators!=null)
                {
                    var any = filterOperators.Any(a => a== filterRequest.Operator);
                    if (!any)
                        throw new InvalidOperationException($"Filtering not allowed Operator");
                }
            }
        }
    }

    private void GuardOnNullFilterRequest(List<FilterRequest>? filterRequests)
    {
        if (filterRequests is {Count: 0})
            throw new InvalidOperationException($"FilterRequest empty");
    }

    private void GuardOnSharpMapperOnly<T>(List<FilterRequest>? filterRequests,List<FilterSharpMapper> filterSharps, FilterSharpMapperBuilder<T> entity)
    {

        if (!entity.GetSharpMapperOnly().State) return;

        var fields = filterRequests!.Select(field=>field.Field).ToList();
        var fieldNames = filterSharps.Select(field=>field.FilterFieldName);
        
        var invalidFields = fields.Except(fieldNames).Any();

        if (!invalidFields) return;

        _abstractBehaviorDataRequestProcessor?.ExceptionHandler(filterRequests!, filterSharps, entity);

        throw new SharpMapperOnlyException($"Invalid fields detected: {string.Join(", ", invalidFields)}");
    }
}