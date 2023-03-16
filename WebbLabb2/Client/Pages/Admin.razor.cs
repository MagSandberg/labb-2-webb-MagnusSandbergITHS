using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Admin : ComponentBase
{
    public List<CustomerDto> AllCustomers { get; set; }
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    //TODO När man går in på admin all products länkas man vidare med currentproduct till edit product
    protected override async Task OnInitializedAsync()
    {
        AllCustomers = new List<CustomerDto>();
        //TODO Ändra till den skyddade HTTP-clienten innan live
        var response = await PublicClient.Client.GetFromJsonAsync<CustomerDto[]>("getAllCustomers");

        if (response != null)
        {
            AllCustomers.AddRange(response);
        }

        await base.OnInitializedAsync();
    }

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