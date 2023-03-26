using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Products : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public List<ProductDto>? AllProducts { get; set; }
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAllProductsAndPopulateList();
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
        var product = await GetProductByName(productName);
        
        var result = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/641dfe3a24041c76e93e812f");
        Order = result;
        
        if (product != null)
        {
            product.OrderCount = 1;
            Order.ProductList.Add(product);
        }

        await PublicClient.Client.PatchAsJsonAsync($"updateOrder?id=641dfe3a24041c76e93e812f", Order);
    }

    private async Task<ProductDto?> GetProductByName(string productName)
    {
        var product = await PublicClient.Client.GetFromJsonAsync<ProductDto>($"getProductByName/{productName}");
        return product;
    }

    private void OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            ShowDialog = false;
        }

        ShowDialog = false;
    }
}