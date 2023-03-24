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
            response = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getOrder/{OrderId}");
            if (response == null)
            {
                //Order;
            }
            Order = response!;
            SelectedOrderId = response!.OrderId;
        }
    }

    //public async Task UpdateCustomer()
    //{
    //    SelectedCustomerId = Customer.Id;

    //    var safetyDto = Customer;

    //    if (Customer.FirstName == string.Empty) { Customer.FirstName = safetyDto.FirstName; }
    //    if (Customer.LastName == string.Empty) { Customer.LastName = safetyDto.LastName; }
    //    if (Customer.Email == string.Empty) { Customer.Email = safetyDto.Email; }
    //    if (Customer.CellNumber == string.Empty) { Customer.CellNumber = safetyDto.CellNumber; }
    //    if (Customer.ZipCode == 0) { Customer.ZipCode = safetyDto.ZipCode; }
    //    if (Customer.City == string.Empty) { Customer.City = safetyDto.City; }
    //    if (Customer.StreetAddress == string.Empty) { Customer.StreetAddress = safetyDto.StreetAddress; }

    //    await PublicClient.Client.PatchAsJsonAsync($"updateCustomer?id={SelectedCustomerId}", Customer);
    //}

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