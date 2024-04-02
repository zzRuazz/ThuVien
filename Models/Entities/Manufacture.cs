namespace Models.Entities;

/// <summary>
/// Manufacture
/// </summary>
public class Manufacture : BaseEntity
{
    /// <summary>
    /// Manufacture Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Manufacture Image
    /// </summary>
    public string Image { get; set; } = null!;

    /// <summary>
    /// Manufacture slug
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Manufacture Website
    /// </summary>
    public string Website { get; set; } = null!;
}
