namespace Models.Entities;

/// <summary>
/// Product
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Foreign key of category
    /// </summary>
    public string CategoryId { get; set; } = null!;

    /// <summary>
    /// Category name
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Foreign key of Manufacture
    /// </summary>
    public string ManufactureId { get; set; } = null!;

    /// <summary>
    /// Manufacture name
    /// </summary>
    public string? ManufactureName { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Product slug
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Product feature image
    /// </summary>
    public string FeatureImage { get; set; } = null!;

    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }
}
