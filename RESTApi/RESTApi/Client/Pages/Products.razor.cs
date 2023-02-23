using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RESTApi.Shared.DTOs;

namespace RESTApi.Client.Pages;

public partial class Products : ComponentBase
{
    public ProductDto CurrentProduct { get; set; } = new();

    private async Task SaveProduct()
    {
        await HttpClient.PostAsJsonAsync("createProduct", CurrentProduct);
    }
}