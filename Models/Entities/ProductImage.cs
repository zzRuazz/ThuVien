namespace Models.Entities;

/// <summary>
/// ProductImage
/// </summary>
public class ProductImage : BaseEntity
{
    /// <summary>
    /// Foreign key of Product
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Product Image
    /// </summary>
    public string Image { get; set; } = null!;
}
