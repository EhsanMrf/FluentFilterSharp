using FilterSharp.Enum;
using FilterSharp.FluentSharp;
using FilterSharp.Input;

namespace FilterSharp.DataProcessing.ProcessorRequest;

public class DataRequestProcessor : IDataRequestProcessor
{
    public void ApplyChanges<T>(DataRequest request, FilterSharpMapperBuilder<T> builder) where T : class
    {
        var filterSharpMappers = builder.GetSharpMappers().ToList();
        if (!filterSharpMappers.Any()) return;

        var filters = request.Filters.ToList();

        foreach (var filterSharpMapper in filterSharpMappers)
        {
            var filter = filters.FirstOrDefault(x => x.Field.Equals(filterSharpMapper.FilterFieldName));
            if (filter != null)
            {
                filter.Field = filterSharpMapper.GetField();

                if (!filterSharpMapper.CanFilter && filter.Operator != null)
                    throw new InvalidOperationException("Filtering not allowed.");

                if (filterSharpMapper.CanOperatorNames is {Count: > 0})
                {
                    if (System.Enum.TryParse<FilterOperator>(filter.Operator, out var parsedOperator))
                    {
                        if (!filterSharpMapper.CanOperatorNames.Contains(parsedOperator))
                        {
                            throw new InvalidOperationException($"Invalid operator: {filter.Operator}");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid operator format.");
                    }
                }
            }
        }
    }
}