namespace Models.Filter;

public class ProductPropertyFilter : FilterBase
{
    /// <summary>
    /// Foreign key of category
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// Property Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Property Name
    /// </summary>
    public string? Slug { get; set; }
}
