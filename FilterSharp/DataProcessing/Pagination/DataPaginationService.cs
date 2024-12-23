using FilterSharp.TransActionService;

namespace FilterSharp.DataProcessing.Pagination;

public sealed class DataPaginationService(PaginationOptions paginationOptions) : IDataPaginationService,ISingletonService
{
    public IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, int? pageNumber, int? pageSize) where T : class
    {
        var size = GetPageSize(pageSize);
        return queryable.Skip((GetPageNumber(pageNumber) - 1) * size).Take(size);
    }


    public int GetPageNumber(int? pageNumber)
    {
        return pageNumber ??paginationOptions.DefaultPageNumber;
    }

    public int GetPageSize(int? pageSize)
    {
        return pageSize is null or 0 ? paginationOptions.DefaultPageSize : pageSize.Value;
    }
}