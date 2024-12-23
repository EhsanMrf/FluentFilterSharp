using FilterSharp.DataProcessing.ChangeRequest;
using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.DataProcessing.Sorting;
using FilterSharp.Input;
using FilterSharp.TransActionService;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp.DataProcessing;

public sealed class DataQueryProcessor : IDataQueryProcessor,ISingletonService
{
    private readonly IApplyChangesDataRequest _applyChangesDataRequest = null!;
    private readonly IDataPaginationService _dataPaginationService = null!;
    private readonly IDataFilterService _filterService = null!;
    private readonly IDataSortingService _sortingService = null!;

    private DataQueryProcessor()
    {
    }

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
        IQueryable<T> queryable, DataQueryRequest? queryRequest) where T : class
    {
        var queryChange = await ApplyChangeDataRequest<T>(queryable, queryRequest);
        var data = await queryChange.queryable.ToListAsync();

        return (data, queryChange.pageNumber, queryChange.pageSize, queryChange.totalCount);
    }
    
    public async Task<(object items, int page, int pageSize, int totalCount)> ApplyQueryRequestItemsTypeObjectAsync<T>(
        IQueryable<T> queryable, DataQueryRequest? queryRequest) where T : class
    {
        var queryChange = await ApplyChangeDataRequest<T>(queryable, queryRequest);
        var data = await ProcessResultItem.GenerateItems<T>(queryChange.queryable, queryRequest);

        return (data, queryChange.pageNumber, queryChange.pageSize, queryChange.totalCount);
    }


    private async Task<(IQueryable<T> queryable, int totalCount, int pageNumber, int pageSize)>
        ApplyChangeDataRequest<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest) where T : class
    {
        queryable = ApplyQueryIfQueryRequestNotNull(queryable, queryRequest);
        var totalCount = await CountDataAsync(queryable);
        queryable = ApplyPagination(queryable, queryRequest, out var pageNumber, out var pageSize);

        return (queryable, totalCount, pageNumber, pageSize);
    }

    private IQueryable<T> ApplyQueryIfQueryRequestNotNull<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest)
        where T : class
    {
        if (queryRequest is null)
            return queryable;

        GetDataChange<T>(queryRequest);
        queryable = ApplyFilters(queryable, queryRequest);
        queryable = ApplySorting(queryable, queryRequest);

        return queryable;
    }

    private void GetDataChange<T>(DataQueryRequest? queryRequest) where T : class
    {
        _applyChangesDataRequest.ApplyDataChangesWithValidation<T>(queryRequest);
    }


    private IQueryable<T> ApplyFilters<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest) where T : class
    {
        return _filterService.ApplyFilters(queryable, queryRequest?.Filters?.ToList());
    }

    private IQueryable<T> ApplySorting<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest)
    {
        return _sortingService.ApplyOrderByMultiple(queryable, queryRequest?.Sorting?.ToList());
    }

    private IQueryable<T> ApplyPagination<T>(IQueryable<T> queryable, DataQueryRequest? queryRequest,
        out int pageNumber, out int pageSize) where T : class
    {
        pageNumber = _dataPaginationService.GetPageNumber(queryRequest?.PageNumber);
        pageSize = _dataPaginationService.GetPageSize(queryRequest?.PageSize);
        return _dataPaginationService.ApplyPagination(queryable, queryRequest?.PageNumber, queryRequest?.PageSize);
    }

    private async Task<List<T>> FetchDataAsync<T>(IQueryable<T> queryable)
    {
        return await queryable.ToListAsync();
    }

    private Task<int> CountDataAsync<T>(IQueryable<T> queryable)
    {
        return queryable.CountAsync();
    }
}