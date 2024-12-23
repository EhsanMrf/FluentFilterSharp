using System.Linq.Dynamic.Core;
using FilterSharp.Input;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp.DataProcessing;

public static class ProcessResultItem
{
    internal static async Task<object> GenerateItems<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest)
    {
        return await GetData(queryable, queryRequest);
    }

    private static async Task<object> GetData<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest)
    {
        if (queryRequest?.Selects is not null && queryRequest.Selects.Count != 0)
        {
            var selectString = "new (" + string.Join(", ", queryRequest.Selects) + ")";
            return await queryable.Select(selectString).ToDynamicListAsync();
        }

        return await queryable.ToListAsync();
    }
}