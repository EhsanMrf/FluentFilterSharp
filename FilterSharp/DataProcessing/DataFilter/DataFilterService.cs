using FilterSharp.Filter;
using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.DataFilter;

public class DataFilterService : IDataFilterService,ISingletonService
{
    public IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, ICollection<Input.FilterRequest>? filters)
        where T : class
    {
        if (filters is null || !filters.Any()) return queryable;

        var predicate = ExpressionFilterBuilder<T>.Build(filters.ToList());
        queryable = queryable.Where(predicate);

        return queryable;
    }
}