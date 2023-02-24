using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Client.Pages;

public partial class Products : ComponentBase
{
    public ProductDto CurrentProduct { get; set; } = new();
    public List<ProductDto> AllProducts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AllProducts = new List<ProductDto>();

        var response = await HttpClient
            .GetFromJsonAsync<ProductDto[]>("getAllProducts");

        if (response != null)
        {
            AllProducts.AddRange(response);
        }

        await base.OnInitializedAsync();
    }

    private async Task SaveProduct()
    {
        await HttpClient.PostAsJsonAsync("createProduct", CurrentProduct);
    }
}