namespace Models.ViewModels;

public class ProductDetailViewModel
{
    public string Id { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string ProductPropertyId { get; set; } = null!;

    public string? ProductProperty { get; set; }

    public string Value { get; set; } = null!;
}
