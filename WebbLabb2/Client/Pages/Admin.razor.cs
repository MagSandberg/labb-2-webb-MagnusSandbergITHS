using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class Admin : ComponentBase
{
    public List<CustomerDto> AllCustomers { get; set; }
    public CustomerDto Customer { get; set; } = new();

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

    private async Task GetCustomerByEmail()
    {
        await PublicClient.Client.PostAsJsonAsync("getCustomerByEmail", Customer.Email);
    }
}