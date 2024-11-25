using FilterSharp.Caching;
using FilterSharp.Filter;
using FilterSharp.Input;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp.DataProcessing;

public static class DataQueryProcessor
{
    public static async Task<(List<T> items, int page, int pageSize, int totalCount)> ToDataSourceResultAsync<T>(
        this IQueryable<T> queryable, DataRequest request,MapperCacheManager cacheManager) where T : class
    {
        
        var mapper = cacheManager.GetMapper<T>();

        
        if (request?.Filters?.Count() > 0)
        {
            var predicate = ExpressionFilterBuilder<T>.Build(request!.Filters.ToList());
            queryable = queryable.Where(predicate);
        }

        try
        {
            var pageNumber = request?.PageNumber ?? 1;
            var pageSize = (request?.PageSize ?? 0) == 0 ? 15 : request.PageSize;

            // Combine the data fetching and count in one query
            var queryResult = await queryable
                .Select(item => new { item, totalCount = queryable.Count() })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Extract data and total count from the query result
            var data = queryResult.Select(x => x.item).ToList();
            var total = queryResult.FirstOrDefault()?.totalCount ?? 0;

            return (data, pageNumber, pageSize, total);
        }

        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}