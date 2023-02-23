namespace WebbLabb2RestApi.Shared.DTOs;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public int ProductNumber { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public int ProductPrice { get; set; }
    public string ProductCategory { get; set; } = string.Empty;
    public bool ProductStatus { get; set; }
}