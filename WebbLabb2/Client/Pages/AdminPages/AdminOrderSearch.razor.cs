using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.AdminPages;

public partial class AdminOrderSearch : ComponentBase
{
    public OrderDto Order { get; set; } = new();
    public string OrderId { get; set; } = string.Empty;
    public string SelectedOrderId { get; set; } = string.Empty;
    private bool ShowDialog { get; set; }

    private async Task GetOrderById()
    {
        var response = new OrderDto();

        SelectedOrderId = string.Empty;

        if (string.IsNullOrEmpty(OrderId) || string.IsNullOrWhiteSpace(OrderId))
        {
            OrderId = "Please enter a valid ID";
            Order = new OrderDto();
        }
        else
        {
            //TODO Fixa API anropet!
            response = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/{OrderId}");
            if (response == null)
            {
                //Order;
            }
            Order = response!;
            SelectedOrderId = response!.OrderId;
        }
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await PublicClient.Client.DeleteAsync($"removeOrder/{SelectedOrderId}");
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