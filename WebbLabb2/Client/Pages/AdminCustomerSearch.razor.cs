using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;
using System.Net.Http.Json;


namespace WebbLabb2.Client.Pages;

public partial class AdminCustomerSearch : ComponentBase
{
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    public Guid SelectedCustomerId { get; set; } = Guid.Empty;
    private bool ShowDialog { get; set; }

    public async Task GetCustomerByEmail()
    {
        var response = new CustomerDto();

        if (string.IsNullOrEmpty(Email))
        {
            Email = "Please enter a valid email";
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<CustomerDto>($"getCustomerByEmail/{Email}");
            Customer = response;
        }
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            SelectedCustomerId = Customer.Id;
            await PublicClient.Client.DeleteAsync($"removeCustomer/{SelectedCustomerId}");
        }

        ShowDialog = false;
    }

    private async Task Close(bool confirmed)
    {
        ShowDialog = false;
    }
}