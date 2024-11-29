namespace FilterSharp.DataProcessing.DataFilter;

public interface IDataFilterService
{
    IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, IEnumerable<Input.Filter> filters) where T : class;
}