using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Products : ComponentBase
{
    public Index _setPlaceholder = new();
    public OrderDto Order { get; set; } = new();
    public List<ProductDto>? AllProducts { get; set; }
    public string PlaceholderEmail = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await GetAllProductsAndPopulateList();
        PlaceholderEmail = _setPlaceholder.PlaceholderEmail;

        await base.OnInitializedAsync();
    }

    private async Task GetAllProductsAndPopulateList()
    {
        AllProducts = new List<ProductDto>();

        var response = await PublicClient.Client.GetFromJsonAsync<ProductDto[]>("getAllProducts");

        if (response != null)
        {
            AllProducts.AddRange(response);
        }
    }
    //TODO Fixa så att produkter läggs till i rätt order
    private async Task AddProductToCart()
    {
        await PublicClient.Client.PatchAsJsonAsync($"updatePlaceholderOrder?email={PlaceholderEmail}", Order);
    }
}