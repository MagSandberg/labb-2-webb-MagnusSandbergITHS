namespace WebbLabb2.Shared.DTOs;

public class ProductDto
{
    public string ProductId { get; set; } = string.Empty;
    public int ProductNumber { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public float ProductPrice { get; set; }
    public string ProductCategory { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public bool ProductStatus { get; set; }
    public string ProductImage { get; set; } = string.Empty;
}