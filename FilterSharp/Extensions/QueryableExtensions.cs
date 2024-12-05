using FilterSharp.DataProcessing;
using FilterSharp.DependencyInjection;
using FilterSharp.Input;

namespace FilterSharp.Extensions;

public static class QueryableExtensions
{
    public static async Task<(List<T> items, int page, int pageSize, int totalCount)> ApplyQueryRequestAsync<T>(
        this IQueryable<T> queryable, 
        DataQueryRequest queryRequest) where T : class
    {
        var processor = ServiceLocator.GetService<DataQueryProcessor>();
        return await processor.ApplyQueryRequestAsync(queryable, queryRequest);
    }
}