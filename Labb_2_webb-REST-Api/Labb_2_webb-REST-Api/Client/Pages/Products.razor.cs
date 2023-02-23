using Microsoft.AspNetCore.Components;
using WebbutvecklingLabb2RESTApi.DataAccess.Services;
using WebbutvecklingLabb2RESTApi.Shared.DTOs;

namespace WebbutvecklingLabb2RESTApi.Client.Pages;

public partial class Products : ComponentBase
{
    public ProductDto CurrentProduct { get; set; } = new();
    private readonly ProductService _service;

    private async Task SaveProduct()
    {
        await _service.AddProduct(CurrentProduct);
    }
}