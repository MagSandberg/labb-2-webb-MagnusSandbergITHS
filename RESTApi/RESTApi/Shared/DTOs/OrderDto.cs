namespace RESTApi.Shared.DTOs;

public class OrderDto
{
    public string CustomerEmail { get; set; } = string.Empty;
    public List<string> ProductList { get; set; } = new();
}