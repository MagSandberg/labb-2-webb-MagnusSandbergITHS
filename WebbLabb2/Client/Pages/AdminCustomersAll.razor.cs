using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;
using WebbLabb2.Shared.Services;

namespace WebbLabb2.Client.Pages;

public partial class AdminCustomersAll : ComponentBase
{
    public List<CustomerDto> AllCustomers { get; set; }
    public  CurrentCustomerService CurrentCustomerService { get; set; } = new();

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
}