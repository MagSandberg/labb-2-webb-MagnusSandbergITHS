using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;
using System.Net.Http.Json;


namespace WebbLabb2.Client.Pages;

public partial class AdminCustomerSearch : ComponentBase
{
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;

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
}