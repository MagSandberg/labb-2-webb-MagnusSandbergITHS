using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;
using WebbLabb2.Shared.Services;

namespace WebbLabb2.Client.Pages;

public partial class AdminCustomersAll : ComponentBase
{
    public List<CustomerDto> AllCustomers { get; set; }
    public string Email { get; set; }
    public Guid SelectedCustomerId { get; set; } = Guid.Empty;
    private bool ShowDialog { get; set; }

    //TODO När man går in på admin all products länkas man vidare med currentproduct till edit product
    protected override async Task OnInitializedAsync()
    {
        AllCustomers = new List<CustomerDto>();
        //TODO Skapa en knapp som sumbittar till en funktion som sparar till currentcustomeremail
        //TODO Ändra till den skyddade HTTP-clienten innan live
        var response = await PublicClient.Client.GetFromJsonAsync<CustomerDto[]>("getAllCustomers");

        if (response != null)
        {
            AllCustomers.AddRange(response);
        }

        await base.OnInitializedAsync();
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await PublicClient.Client.DeleteAsync($"removeCustomer/{SelectedCustomerId}");
        }

        ShowDialog = false;
    }

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }

    public void SetCurrentCustomerId(Guid id)
    {
        SelectedCustomerId = id;
    }
    public void SetCurrentCustomerEmail(string email)
    {
        CurrentCustomerService.CurrentCustomerEmail = email;
    }
}