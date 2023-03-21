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

        if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
        {
            Email = "Please enter a valid email";
            Customer = new CustomerDto();
        }
        else
        {
            //TODO Ta in hela responset utan att parsa den för att komma åt result
            response = await PublicClient.Client.GetFromJsonAsync<CustomerDto>($"getCustomerByEmail/{Email}");

            Customer = response!;
            SelectedCustomerEmail = response!.Email;
        }
    }

    public async Task UpdateCustomer()
    {
        SelectedCustomerId = Customer.Id;

        var safetyDto = Customer;

        if (Customer.FirstName == string.Empty) { Customer.FirstName = safetyDto.FirstName; }
        if (Customer.LastName == string.Empty) { Customer.LastName = safetyDto.LastName; }
        if (Customer.Email == string.Empty) { Customer.Email = safetyDto.Email; }
        if (Customer.CellNumber == string.Empty) { Customer.CellNumber = safetyDto.CellNumber; }
        if (Customer.ZipCode == 0) { Customer.ZipCode = safetyDto.ZipCode; }
        if (Customer.City == string.Empty) { Customer.City = safetyDto.City; }
        if (Customer.StreetAddress == string.Empty) { Customer.StreetAddress = safetyDto.StreetAddress; }

        await PublicClient.Client.PatchAsJsonAsync($"updateCustomer?id={SelectedCustomerId}", Customer);
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
}