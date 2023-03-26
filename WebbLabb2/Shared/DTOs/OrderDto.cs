namespace WebbLabb2.Shared.DTOs;

public class OrderDto
{
    public string OrderId { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public List<ProductDto>? ProductList { get; set; } = new();
}