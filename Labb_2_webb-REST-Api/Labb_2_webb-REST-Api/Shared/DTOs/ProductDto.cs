namespace WebbutvecklingLabb2RESTApi.Shared.DTOs;

public class ProductDto
{
    public int ProductNumber { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public int ProductPrice { get; set; }
    public string ProductCategory { get; set; } = string.Empty;
    public bool ProductStatus { get; set; }
}