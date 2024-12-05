using FilterSharp.Input;

namespace FilterSharp;

public abstract class DataQueryBase
{
    public async Task<(List<T> items, int page, int pageSize, int totalCount)> ExecuteQueryAsync<T>(
        IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class
    {
       
        GetDataChange<T>(queryRequest);
        queryable = ApplyFilters(queryable, queryRequest);
        queryable = ApplySorting(queryable, queryRequest);
        queryable = ApplyPagination(queryable, queryRequest, out var pageNumber, out var pageSize);
        
        var data = await FetchDataAsync(queryable);
        var totalCount = await CountDataAsync(queryable);

        return (data, pageNumber, pageSize, totalCount);
    }



    protected abstract void GetDataChange<T>(DataQueryRequest queryRequest) where T : class;
    protected abstract IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class;
    protected abstract IQueryable<T> ApplySorting<T>(IQueryable<T> queryable, DataQueryRequest queryRequest);
    protected abstract IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, DataQueryRequest queryRequest, out int pageNumber, out int pageSize) where T : class;
    protected abstract Task<List<T>> FetchDataAsync<T>(IQueryable<T> queryable) where T : class;
    protected abstract Task<int> CountDataAsync<T>(IQueryable<T> queryable) where T : class;
}