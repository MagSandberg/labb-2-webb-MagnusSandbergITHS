using Microsoft.AspNetCore.Components;
using RESTApi.Shared.DTOs;
using RESTApi.DataAccess.Services;

namespace RESTApi.Client.Pages;

public partial class Products : ComponentBase
{
    public ProductDto CurrentProduct { get; set; } = new();
    private readonly ProductService _service;

    private async Task SaveProduct()
    {
        await _service.AddProduct(CurrentProduct);
    }
}