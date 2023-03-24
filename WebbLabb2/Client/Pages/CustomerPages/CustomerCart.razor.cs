using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.CustomerPages;

public partial class CustomerCart : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public string CurrentOrderId { get; set; } = string.Empty;
    private bool ShouldShowContent { get; set; }
    private bool ShowDialog { get; set; }

    const string placeholderEmail = "placeholder@email.com";

    protected override async Task OnInitializedAsync()
    {
        await CreatePlaceholderOrder();

        ShowContent = ShouldShowContent;

        await base.OnInitializedAsync();
    }

    private async Task DeletePlaceholderOrder(string email)
    {

    }

    private async Task CreatePlaceholderOrder()
    {
        var placeholderList = new List<ProductDto>();

        Order.CustomerEmail = placeholderEmail;
        Order.ProductList = placeholderList;

        await PublicClient.Client.PostAsJsonAsync("createOrder", Order);
    }

    public async Task UpdateOrder()
    {
        CurrentOrderId = Order.OrderId;
        var safetyDto = Order;

        if (Order.CustomerEmail == string.Empty) { Order.CustomerEmail = safetyDto.CustomerEmail; }

        await PublicClient.Client.PatchAsJsonAsync($"updateOrder?id={CurrentOrderId}", Order);
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await PublicClient.Client.DeleteAsync($"removeOrder/{CurrentOrderId}");
        }

        ShowDialog = false;
    }

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }

    public void RemoveProductAtIndex(string productName)
    {
        var filter = Order.ProductList!.FindIndex(p => p.ProductName == productName);
        Order.ProductList.RemoveAt(filter);
    }
}