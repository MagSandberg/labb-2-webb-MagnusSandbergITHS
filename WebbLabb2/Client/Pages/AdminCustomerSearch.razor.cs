using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;
using System.Net.Http.Json;


namespace WebbLabb2.Client.Pages;

public partial class AdminCustomerSearch : ComponentBase
{
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    public string SelectedCustomerEmail { get; set; } = string.Empty;
    public Guid SelectedCustomerId { get; set; } = Guid.Empty;
    private bool ShowDialog { get; set; }

    private async Task GetCustomerByEmail()
    {
        var response = new CustomerDto();

        SelectedCustomerEmail = string.Empty;

        if (string.IsNullOrEmpty(Email))
        {
            Email = "Please enter a valid email";
            Customer = new CustomerDto();
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<CustomerDto>($"getCustomerByEmail/{Email}");

            Customer = response!;
            SelectedCustomerEmail = response!.Email;
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

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }
}