using FilterSharp.Input;

namespace FilterSharp.DataProcessing.Sorting;

public interface IDataSortingService
{
    IQueryable<T> ApplyOrderByMultiple<T>(IQueryable<T> query, List<SortingRequest>? orderByClauses);
}