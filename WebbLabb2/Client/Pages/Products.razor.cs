using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Products : ComponentBase
{
    public Index SetPlaceholder = new();
    public OrderDto Order { get; set; } = new();
    public List<ProductDto>? AllProducts { get; set; }
    public string PlaceholderEmail = string.Empty;
    public string CurrentOrderId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await GetAllProductsAndPopulateList();
        await GetPlaceholderOrder();

        PlaceholderEmail = SetPlaceholder.PlaceholderEmail;

        await base.OnInitializedAsync();
    }
    //TODO Varför måste just detta api-anropet ha en JSON token???
    private async Task GetPlaceholderOrder()
    {
        var result = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getPlaceholderOrder/{PlaceholderEmail}");

        if (result != null)
        {
            CurrentOrderId = result.OrderId;
        }
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