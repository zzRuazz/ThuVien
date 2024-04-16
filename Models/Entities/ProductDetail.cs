namespace Models.Entities;

/// <summary>
/// ProductDetail
/// </summary>
public class ProductDetail : BaseEntity
{
    /// <summary>
    /// Foreign key of Product
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Foreign key of category
    /// </summary>
    public string CategoryId { get; set; }

    /// <summary>
    /// Foreign key of ProductProperty
    /// </summary>
    public string ProductPropertyId { get; set; } = null!;

    /// <summary>
    /// ProductProperty
    /// </summary>
    public string? ProductProperty { get; set; }

    /// <summary>
    /// Value of ProductProperty
    /// </summary>
    public string? Value { get; set; }
}
