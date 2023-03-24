using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.CustomerPages;

public partial class CustomerCart : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public string CurrentOrderId { get; set; } = string.Empty;
    private bool ShouldShowContent { get; set; }
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {

        if (CurrentOrderId.IsNullOrEmpty())
        {
            CurrentOrderId = Order.OrderId;
        }

        ShowContent = ShouldShowContent;

        await base.OnInitializedAsync();
    }
    //TODO Lägg till get order placeholderorder
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