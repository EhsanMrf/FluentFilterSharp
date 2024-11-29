namespace FilterSharp.DataProcessing.Pagination;

public interface IDataPaginationService
{
    IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, int? pageNumber, int? pageSize) where T : class;
     int GetPageSize(int? pageSize);
     int GetPageNumber(int? pageNumber);
}