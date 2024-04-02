namespace Models.Common;

public class DataPaging
{
    public IEnumerable<object>? Data { get; set; }

    public long? PaginationCount { get; set; }
}

public class DataPaging<T>
{
    public IEnumerable<T>? Data { get; set; }

    public long? PaginationCount { get; set; }
}
