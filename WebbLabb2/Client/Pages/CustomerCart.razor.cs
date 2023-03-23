using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class CustomerCart : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public string CurrentOrderId { get; set; } = string.Empty;
    private bool ShouldShowContent { get; set; }
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentOrderId = await SessionStorage.GetItemAsync<string>("orderID");

        ShowContent = ShouldShowContent;

        await base.OnInitializedAsync();
    }

    private async Task CreateOrder()
    {
        //Order.CustomerEmail = SessionStorage.
        var response = PublicClient.Client.PostAsJsonAsync("createOrder", Order);
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