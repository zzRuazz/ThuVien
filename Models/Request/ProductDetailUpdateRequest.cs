namespace Models.Request;

public class ProductDetailUpdateRequest
{
    public string? Id { get; set; }

    public string? ProductId { get; set; }

    public string? ProductPropertyId { get; set; }

    public string? Value { get; set; }
}
