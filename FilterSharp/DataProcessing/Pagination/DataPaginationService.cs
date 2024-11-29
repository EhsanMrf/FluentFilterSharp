namespace FilterSharp.DataProcessing.Pagination;

public class DataPaginationService : IDataPaginationService
{
    public IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, int? pageNumber, int? pageSize) where T : class
    {
        var size = GetPageSize(pageSize);
        return queryable.Skip((GetPageNumber(pageNumber) - 1) * size).Take(size);
    }


    public int GetPageNumber(int? pageNumber)
    {
        return pageNumber ?? 0;
    }

    public int GetPageSize(int? pageSize)
    {
        return pageSize is null or 0 ? 15 : pageSize.Value;
    }
}