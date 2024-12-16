namespace FilterSharp.DataProcessing.Pagination;

public sealed class PaginationOptions
{
    /// <summary>
    /// default number 0
    /// </summary>
    internal int DefaultPageNumber { get; set; } = 0; 
    
    /// <summary>
    /// default number 15
    /// </summary>
    public int DefaultPageSize { get; set; } = 15;
}