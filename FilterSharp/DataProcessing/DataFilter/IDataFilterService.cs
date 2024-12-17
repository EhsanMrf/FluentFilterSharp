namespace FilterSharp.DataProcessing.DataFilter;

public interface IDataFilterService
{
    IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, ICollection<Input.FilterRequest>? filters) where T : class;
}