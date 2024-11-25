namespace FilterSharp.Extensions;

public static class QueryableExtensions
{
    public static Task<object>? ToDataSourceResultAsync<T>(this IQueryable<T> queryable)
    {
        return null;
    }
}