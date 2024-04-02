namespace Models.ViewModels;

/// <summary>
/// CategoryViewModel
/// </summary>
public class CategoryViewModel
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Category Image
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Slug
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// ParentId
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// Children
    /// </summary>
    public List<CategoryViewModel>? Children { get; set; }
}
