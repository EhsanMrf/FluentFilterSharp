namespace FilterSharpTest;

public sealed class QueryResult<T>(object items, int page, int pageSize, int totalCount)
{
    public object Items { get; private set; } = items;
    public int Page { get; private set; } = page;
    public int PageSize { get; private set; } = pageSize;
    public int TotalCount { get; private set; } = totalCount;
}