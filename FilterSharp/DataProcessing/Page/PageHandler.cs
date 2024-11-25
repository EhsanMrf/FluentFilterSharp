namespace FilterSharp.DataProcessing.Page;

public static class PageHandler
{
    /// <summary>
    ///  default => page : 0 , pageSize : 10
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="page"> default 0</param>
    /// <param name="pageSize"> default 10</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> Pagination<T>(this IQueryable<T> queryable, int page, int pageSize)
    {
        return queryable.Skip(page * pageSize).Take(pageSize);
    }
}