namespace Models.Entities;

/// <summary>
/// Category
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Category Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Category Image
    /// </summary>
    public string Image { get; set; } = null!;

    /// <summary>
    /// Category slug
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Category parent Id
    /// </summary>
    public string? ParentId { get; set; }
}
