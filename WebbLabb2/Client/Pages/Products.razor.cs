using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Products : ComponentBase
{
    public Index SetPlaceholder = new();
    public OrderDto Order { get; set; } = new();
    public ProductDto Product { get; set; } = new();
    public List<ProductDto>? AllProducts { get; set; }
    public string PlaceholderEmail = string.Empty;
    public string PlaceholderÍd = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await GetAllProductsAndPopulateList();
        PlaceholderEmail = SetPlaceholder.PlaceholderEmail;
        PlaceholderÍd = SetPlaceholder.CurrentOrderId;
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
    //TODO UNIT OF WORK HÄR?
    private async Task AddProductToCart(string productName)
    {
        var product = await PublicClient.Client.GetFromJsonAsync<ProductDto>($"getProductByName/{productName}");

        if (product != null)
        {
            Order.ProductList.Add(product);
        }

        await PublicClient.Client.PatchAsJsonAsync($"updatePlaceholderOrder?email={PlaceholderEmail}", Order);
    }
}