using FilterSharp.DependencyInjection.Locator;
using FilterSharp.Input;

namespace FilterSharpTest;

public static class QueryApply
{
    public static async Task<QueryResult<T>> ApplyQueryResult<T>(this IQueryable<T> query,
        DataQueryRequest? queryRequest) where T : class
    {
        var result = await ServiceLocator.DataQueryProcessor.ApplyQueryRequestAsync(query, queryRequest);
        return new QueryResult<T>(result.items, result.page, result.pageSize, result.totalCount);
    }
}