namespace Models.Entities;

/// <summary>
/// ProductProperty
/// </summary>
public class ProductProperty : BaseEntity
{
    /// <summary>
    /// Foreign key of category
    /// </summary>
    public string? CategoryId { get; set; }

    /// <summary>
    /// CategoryName
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Property Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Property Name
    /// </summary>
    public string Slug { get; set; } = null!;
}
