namespace Models.Entities;

/// <summary>
/// Product
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Foreign key of category
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Foreign key of Manufacture
    /// </summary>
    public long ManufactureId { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Product feature image
    /// </summary>
    public string FeatureImage { get; set; } = null!;

    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }
}
