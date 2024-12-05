using FilterSharp.DataProcessing;
using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.DataProcessing.Sorting;
using FilterSharp.Input;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp;

//public interface IAsyncDataQuery
//{
//    Task<(List<T> items, int page, int pageSize, int totalCount)> ExecuteQueryAsync<T>(
//        IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class;
//}
//public class AsyncDataQuery :DataQueryBase,IAsyncDataQuery
//{
//    private readonly IApplyChangesDataRequest _applyChangesDataRequest;
//    private readonly IDataPaginationService _dataPaginationService;
//    private readonly IDataFilterService _filterService;
//    private readonly IDataSortingService _sortingService;

//    public AsyncDataQuery(IApplyChangesDataRequest applyChangesDataRequest, IDataPaginationService dataPaginationService, IDataFilterService filterService, IDataSortingService sortingService)
//    {
//        _applyChangesDataRequest = applyChangesDataRequest;
//        _dataPaginationService = dataPaginationService;
//        _filterService = filterService;
//        _sortingService = sortingService;
//    }
    
//    protected override void GetDataChange<T>(DataQueryRequest queryRequest)
//    {
//        _applyChangesDataRequest.GetDataChange<T>(queryRequest);
//    }

//    protected override IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, DataQueryRequest queryRequest)
//    {
//        return _filterService.ApplyFilters(queryable, queryRequest.Filters);
//    }

//    protected override IQueryable<T> ApplySorting<T>(IQueryable<T> queryable, DataQueryRequest queryRequest)
//    {
//        return _sortingService.ApplyOrderByMultiple(queryable, queryRequest?.Sorting?.ToList());
//    }

//    protected override IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, DataQueryRequest queryRequest, out int pageNumber, out int pageSize)
//    {
//        pageNumber = _dataPaginationService.GetPageNumber(queryRequest?.PageNumber);
//        pageSize = _dataPaginationService.GetPageSize(queryRequest?.PageSize);
//        return _dataPaginationService.ApplyPagination(queryable, queryRequest?.PageNumber, queryRequest?.PageSize);
//    }

//    protected override async Task<List<T>> FetchDataAsync<T>(IQueryable<T> queryable)
//    {
//        return await queryable.ToListAsync();
//    }

//    protected override Task<int> CountDataAsync<T>(IQueryable<T> queryable)
//    {
//        return  queryable.CountAsync();
//    }
//}