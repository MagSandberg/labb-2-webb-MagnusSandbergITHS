using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Client.Shared.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.AdminPages;

public partial class AdminCustomersAll : ComponentBase
{
    public List<CustomerDto>? AllCustomers { get; set; }
    public Guid SelectedCustomerId { get; set; } = Guid.Empty;
    private bool ShowDialog { get; set; }
    //TODO Ändra till den skyddade HTTP-clienten innan live

    protected override async Task OnInitializedAsync()
    {
        await GetAllCustomersAndPopulateList();
        await base.OnInitializedAsync();
    }

    private async Task GetAllCustomersAndPopulateList()
    {
        AllCustomers = new List<CustomerDto>();
        var response = await PublicClient.Client.GetFromJsonAsync<CustomerDto[]>("getAllCustomers");

        if (response != null)
        {
            AllCustomers.AddRange(response);
        }
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await PublicClient.Client.DeleteAsync($"removeCustomer/{SelectedCustomerId}");
            await GetAllCustomersAndPopulateList();
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