using FilterSharp.DependencyInjection.Locator;
using FilterSharp.Dto;
using FilterSharp.Input;

namespace FilterSharp.Extensions;

public static class QueryFilterSharp
{
    /// <summary>
    /// none support selects
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="queryRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<(List<T> items, int page, int pageSize, int totalCount)> ApplyQueryWithDetailsAsync<T>(this IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class
    {
        return await ServiceLocator.DataQueryProcessor.ApplyQueryRequestAsync(queryable, queryRequest);
    }
    
    /// <summary>
    /// support selects
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="queryRequest"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<QueryResult> ApplyQueryAsResultAsync<T>(this IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class
    {
        var data= await ServiceLocator.DataQueryProcessor.ApplyQueryRequestItemsTypeObjectAsync(queryable, queryRequest);
        return new QueryResult(data.items, data.page, data.pageSize, data.totalCount);
    }
}