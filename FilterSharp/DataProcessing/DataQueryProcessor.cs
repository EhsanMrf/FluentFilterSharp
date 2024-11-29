using FilterSharp.DataProcessing.DataFilter;
using FilterSharp.DataProcessing.Pagination;
using FilterSharp.Input;
using Microsoft.EntityFrameworkCore;

namespace FilterSharp.DataProcessing;

public class DataQueryProcessor : IDataQueryProcessor
{
    private readonly IApplyChangesDataRequest _applyChangesDataRequest;
    private readonly IDataPaginationService _dataPaginationService;
    private readonly IDataFilterService _filterService;

    public DataQueryProcessor(IApplyChangesDataRequest applyChangesDataRequest,
        IDataPaginationService dataPaginationService, IDataFilterService filterService)
    {
        _applyChangesDataRequest = applyChangesDataRequest;
        _dataPaginationService = dataPaginationService;
        _filterService = filterService;
    }

    public async Task<(List<T> items, int page, int pageSize, int totalCount)> ApplyDataRequestAsync<T>(
        IQueryable<T> queryable, DataRequest request) where T : class
    {
        queryable = _filterService.ApplyFilters(queryable, request.Filters);

        var pageNumber = _dataPaginationService.GetPageNumber(request?.PageNumber);
        var pageSize = _dataPaginationService.GetPageSize(request?.PageSize);
        queryable = _dataPaginationService.ApplyPagination(queryable, request?.PageNumber, request?.PageSize);


        var data = await queryable.ToListAsync();
        var total = await queryable.CountAsync();

        return (data, pageNumber, pageSize, total);
    }
}