using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.DataProcessing.Sorting;
using FilterSharp.Input;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp.DataProcessing;

public sealed class DataQueryProcessor : IDataQueryProcessor
{
    private readonly IApplyChangesDataRequest _applyChangesDataRequest = null!;
    private readonly IDataPaginationService _dataPaginationService = null!;
    private readonly IDataFilterService _filterService = null!;
    private readonly IDataSortingService _sortingService = null!;

    private DataQueryProcessor() { }
    public DataQueryProcessor(IApplyChangesDataRequest applyChangesDataRequest,
        IDataPaginationService dataPaginationService, IDataFilterService filterService,
        IDataSortingService sortingService)
    {
        _applyChangesDataRequest = applyChangesDataRequest;
        _dataPaginationService = dataPaginationService;
        _filterService = filterService;
        _sortingService = sortingService;
    }

    public async Task<(List<T> items, int page, int pageSize, int totalCount)> ApplyQueryRequestAsync<T>(
        IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class
    {
        GetDataChange<T>(queryRequest);
        queryable = ApplyFilters(queryable, queryRequest);
        queryable = ApplySorting(queryable, queryRequest);
        queryable = ApplyPagination(queryable, queryRequest, out var pageNumber, out var pageSize);
        
        var data = await FetchDataAsync(queryable);
        var totalCount = await CountDataAsync(queryable);

        return (data, pageNumber, pageSize, totalCount);
    }


    private  void GetDataChange<T>(DataQueryRequest queryRequest) where T : class
    {
        _applyChangesDataRequest.GetDataChange<T>(queryRequest);
    }

    
    private  IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, DataQueryRequest queryRequest) where T : class
    {
        return _filterService.ApplyFilters(queryable, queryRequest.Filters);
    }

    private  IQueryable<T> ApplySorting<T>(IQueryable<T> queryable, DataQueryRequest queryRequest)
    {
        return _sortingService.ApplyOrderByMultiple(queryable, queryRequest?.Sorting?.ToList());
    }

    private  IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, DataQueryRequest queryRequest, out int pageNumber, out int pageSize) where T : class
    {
        pageNumber = _dataPaginationService.GetPageNumber(queryRequest?.PageNumber);
        pageSize = _dataPaginationService.GetPageSize(queryRequest?.PageSize);
        return _dataPaginationService.ApplyPagination(queryable, queryRequest?.PageNumber, queryRequest?.PageSize);
    }

    private  async Task<List<T>> FetchDataAsync<T>(IQueryable<T> queryable)
    {
        return await queryable.ToListAsync();
    }

    private Task<int> CountDataAsync<T>(IQueryable<T> queryable)
    {
        return  queryable.CountAsync();
    }  
    
    private List<T> FetchData<T>(IQueryable<T> queryable)
    {
        return  queryable.ToList();
    }

    private int CountData<T>(IQueryable<T> queryable)
    {
        return queryable.Count();
    }
}