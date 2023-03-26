using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.AdminPages;

public partial class AdminOrdersAll : ComponentBase
{
    public List<OrderDto>? AllOrders { get; set; }
    private bool ShowDialog { get; set; }
    public string SelectedOrderId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await GetAllOrdersAndPopulateList();

        await base.OnInitializedAsync();
    }

    private async Task GetAllOrdersAndPopulateList()
    {
        AllOrders = new List<OrderDto>();
        var response = await PublicClient.Client.GetFromJsonAsync<OrderDto[]>("getOrders");

        if (response != null)
        {
            AllOrders.AddRange(response);
        }
    }

    private async Task DeleteOrder()
    {
        await PublicClient.Client.DeleteAsync($"removeOrder/{SelectedOrderId}");
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await DeleteOrder();
            await GetAllOrdersAndPopulateList();
        }

        ShowDialog = false;
    }

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }

    public void SetCurrentOrderId(string id)
    {
        SelectedOrderId = id;
    }
}