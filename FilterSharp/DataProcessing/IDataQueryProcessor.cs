using FilterSharp.Input;

namespace FilterSharp.DataProcessing;

public interface IDataQueryProcessor
{
    /// <summary>
    /// Processes the given queryable data source based on the provided data request parameters,
    /// such as pagination, sorting, and filtering, and returns the resulting data along with 
    /// pagination metadata.
    /// </summary>
    /// <typeparam name="T">The type of the data items in the queryable source.</typeparam>
    /// <param name="queryable">The queryable data source to process.</param>
    /// <param name="request">
    /// The <see cref="DataRequest"/> object containing pagination, filtering, and sorting criteria.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains a tuple with:
    /// - <see cref="List{T}"/>: The list of items after applying the request.
    /// - <see cref="int"/>: The current page number.
    /// - <see cref="int"/>: The page size (number of items per page).
    /// - <see cref="int"/>: The total count of items in the data source matching the criteria.
    /// </returns>
    Task<(List<T> items, int page, int pageSize, int totalCount)> ApplyDataRequestAsync<T>(IQueryable<T> queryable, DataRequest request)where T : class;
    
}