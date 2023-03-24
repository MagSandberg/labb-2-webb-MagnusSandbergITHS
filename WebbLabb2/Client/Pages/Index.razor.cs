using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Index
{
    public OrderDto Order { get; set; } = new();
    public string CurrentOrderId { get; set; } = string.Empty;
    public string PlaceholderEmail = "placeholder@order.page";

    protected override async Task OnInitializedAsync()
    {
        await DeletePlaceholderOrder(PlaceholderEmail);
        await CreatePlaceholderOrder();

        if (CurrentOrderId.IsNullOrEmpty())
        {
            CurrentOrderId = Order.OrderId;
        }

        await base.OnInitializedAsync();
    }

    private async Task DeletePlaceholderOrder(string email)
    {
        await PublicClient.Client.DeleteAsync($"removePlaceholderOrder/{email}");
    }

    private async Task CreatePlaceholderOrder()
    {
        var placeholderList = new List<ProductDto>();

        Order.CustomerEmail = PlaceholderEmail;
        Order.ProductList = placeholderList;

        await PublicClient.Client.PostAsJsonAsync("createPlaceholderOrder", Order);
    }
}