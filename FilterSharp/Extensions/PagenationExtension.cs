namespace FilterSharp.Extensions;

public class PaginationExtension
{
    /// <summary>
    ///  default => page : 0 , pageSize : 10
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="page"> default 0</param>
    /// <param name="pageSize"> default 10</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected static IQueryable<T> PageSize<T>(IQueryable<T> queryable, int page = 0, int pageSize = 10)
    {
        return queryable.Skip(page * pageSize).Take(pageSize);
    }
}