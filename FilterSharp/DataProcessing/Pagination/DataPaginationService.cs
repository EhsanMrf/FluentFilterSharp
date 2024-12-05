namespace FilterSharp.DataProcessing.Pagination;

public class DataPaginationService : IDataPaginationService
{
    private readonly PaginationOptions _paginationOptions;

    public DataPaginationService(PaginationOptions paginationOptions)
    {
        _paginationOptions = paginationOptions;
    }

    public IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, int? pageNumber, int? pageSize) where T : class
    {
        var size = GetPageSize(pageSize);
        return queryable.Skip((GetPageNumber(pageNumber) - 1) * size).Take(size);
    }


    public int GetPageNumber(int? pageNumber)
    {
        return pageNumber ??_paginationOptions.DefaultPageNumber;
    }

    public int GetPageSize(int? pageSize)
    {
        return pageSize ?? _paginationOptions.DefaultPageSize;
    }
}

public class PaginationOptions
{
    /// <summary>
    /// default number 0
    /// </summary>
    public int DefaultPageNumber { get; set; } = 0; 
    
    /// <summary>
    /// default number 15
    /// </summary>
    public int DefaultPageSize { get; set; } = 15;
}