namespace Models.Filter;

public class ProductFilter : FilterBase
{
    /// <summary>
    /// Foreign key of category
    /// </summary>
    public string? CategoryId { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Foreign key of Manufacture
    /// </summary>
    public string? ManufactureId { get; set; }

    /// <summary>
    /// Manufacture name
    /// </summary>
    public string? ManufactureName { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// UpPrice
    /// </summary>
    public long? UpPrice { get; set; }

    /// <summary>
    /// DownPrice
    /// </summary>
    public long? DownPrice { get; set; }
}
