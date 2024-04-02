namespace Models.Filter;

public class ManufactureFilter : FilterBase
{
    /// <summary>
    /// Manufacture Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Manufacture Image
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Manufacture slug
    /// </summary>
    public string? Slug { get; set; }

    /// <summary>
    /// Manufacture Website
    /// </summary>
    public string? Website { get; set; }
}
