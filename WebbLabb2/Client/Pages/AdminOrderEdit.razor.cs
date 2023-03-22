using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminOrderEdit : ComponentBase
{
    public OrderDto CurrentSelectedOrder { get; set; } = new();
    public string CurrentOrderId { get; set; } = string.Empty;
    private bool ShouldShowContent { get; set; }
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentOrderId = await SessionStorage.GetItemAsync<string>("orderID");

        while (true)
        {
            ShouldShowContent = false;
            await GetOrderById();

            await Task.Delay(1000);
            if (!CurrentOrderId.IsNullOrEmpty())
            {
                ShouldShowContent = true;
                break;
            }
        }

        ShowContent = ShouldShowContent;

        await base.OnInitializedAsync();
    }

    public async Task GetOrderById()
    {
        var value = await SessionStorage.GetItemAsync<string>("orderId");
        CurrentOrderId = value;

        if (string.IsNullOrEmpty(CurrentOrderId))
        {
            CurrentSelectedOrder = new OrderDto();
        }
        else
        {
            CurrentSelectedOrder = (await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/{CurrentOrderId}"))!;
        }
    }

    public async Task UpdateOrder()
    {
        CurrentOrderId = CurrentSelectedOrder.OrderId;
        var safetyDto = CurrentSelectedOrder;
        
        if (CurrentSelectedOrder.CustomerEmail == string.Empty) { CurrentSelectedOrder.CustomerEmail = safetyDto.CustomerEmail; }

        await PublicClient.Client.PatchAsJsonAsync($"updateOrder?id={CurrentOrderId}", CurrentSelectedOrder);
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
        var filter = CurrentSelectedOrder.ProductList!.FindIndex(p => p.ProductName == productName);
        CurrentSelectedOrder.ProductList.RemoveAt(filter);
    }
}