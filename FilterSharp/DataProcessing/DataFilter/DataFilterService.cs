using FilterSharp.Filter;

namespace FilterSharp.DataProcessing.DataFilter;

public class DataFilterService : IDataFilterService
{
    public IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, IEnumerable<Input.FilterRequest>? filters)
        where T : class
    {
        if (filters is not null && filters.Any())
        {
            var predicate = ExpressionFilterBuilder<T>.Build(filters.ToList());
            queryable = queryable.Where(predicate);
        }

        return queryable;
    }
}