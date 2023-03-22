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

    protected override async Task OnInitializedAsync()
    {
        CurrentOrderId = await SessionStorage.GetItemAsync<string>("orderID");

        while (true)
        {
            ShouldShowContent = false;
            await GetOrderById(CurrentOrderId);

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

    public async Task GetOrderById(string id)
    {
        var response = new OrderDto();
        var value = string.Empty;

        value = await SessionStorage.GetItemAsync<string>("orderId");
        id = value;
        CurrentOrderId = id;

        if (string.IsNullOrEmpty(id))
        {
            CurrentSelectedOrder = new OrderDto();
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/{CurrentOrderId}");
            CurrentSelectedOrder = response!;
        }
    }

    public async Task UpdateOrder()
    {
        CurrentOrderId = CurrentSelectedOrder.OrderId;

        var safetyDto = CurrentSelectedOrder;

        //if (CurrentSelectedOrder.FirstName == string.Empty) { CurrentSelectedOrder.FirstName = safetyDto.FirstName; }
        //if (CurrentSelectedOrder.OrderId == string.Empty) { CurrentSelectedOrder.LastName = safetyDto.LastName; }
        //if (CurrentSelectedOrder.OrderId == string.Empty) { CurrentSelectedOrder.OrderId = safetyDto.Email; }
        //if (CurrentSelectedOrder.OrderId == string.Empty) { CurrentSelectedOrder = safetyDto.CellNumber; }
        //if (CurrentSelectedOrder.ZipCode == 0) { CurrentSelectedOrder.OrderId = safetyDto.ZipCode; }
        //if (CurrentSelectedOrder.City == string.Empty) { CurrentSelectedOrder.City = safetyDto.City; }
        //if (CurrentSelectedOrder.StreetAddress == string.Empty) { CurrentSelectedOrder.StreetAddress = safetyDto.StreetAddress; }

        //await PublicClient.Client.PatchAsJsonAsync($"updateCustomer?id={CurrentCustomerId}", CurrentSelectedCustomer);
    }
}