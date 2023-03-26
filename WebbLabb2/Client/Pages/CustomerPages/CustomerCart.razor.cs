using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.CustomerPages;

public partial class CustomerCart : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public OrderDto UpdatedOrder { get; set; } = new();
    public string SelectedProductName { get; set; } = string.Empty;
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetPlaceholderOrder();
        await base.OnInitializedAsync();
    }

    public async Task GetPlaceholderOrder()
    {
        var result = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/641dfe3a24041c76e93e812f");
        Order = result;
    }

    public async Task UpdateOrder()
    {
        await PublicClient.Client.PatchAsJsonAsync($"updateOrder?id=641dfe3a24041c76e93e812f", UpdatedOrder);
    }

    public void SetSelectedProductName(string selectedProductName)
    {
        SelectedProductName = selectedProductName;
    }

    public async Task RemoveProductAtIndex(string productName)
    {
        await Task.Run(() =>
        {
            var filter = Order.ProductList!.FindIndex(p => p.ProductName == productName);
            Order.ProductList.RemoveAt(filter);

            UpdatedOrder = Order;
        });
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await RemoveProductAtIndex(SelectedProductName);
            await UpdateOrder();
        }

        ShowDialog = false;
    }

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }
}