namespace DotnetSdk.Common;

public class PaginatedListUtils<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public List<T> Data { get; set; } = [];

    public static PaginatedListUtils<T> Create(List<T> items, int count, int pageIndex, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        
        var paginatedList = new PaginatedListUtils<T>()
        {
            PageIndex = pageIndex,
            TotalPages = totalPages,
            HasPreviousPage = pageIndex > 1,
            HasNextPage = pageIndex < totalPages
        };
        
        paginatedList.Data.AddRange(items);
        return paginatedList;
    }
}