using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.DataFilter;

public interface IDataFilterService: ISingletonService
{
    IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, ICollection<Input.FilterRequest>? filters) where T : class;
}