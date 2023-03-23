using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class CustomerUpdate
{
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var userAuth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        Email = userAuth.User.Identity.Name;
        Customer.Email = Email;

        await GetCustomerByEmail(Email);

        await base.OnInitializedAsync();
    }

    public async Task GetCustomerByEmail(string email)
    {
        var response = new CustomerDto();

        if (string.IsNullOrEmpty(email))
        {
            Customer = new CustomerDto();
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<CustomerDto>($"getCustomerByEmail/{Email}");
            Customer = response!;
        }
    }
    public async Task UpdateCustomer()
    {
        await PublicClient.Client.PatchAsJsonAsync($"updateCustomer?id={Customer.Id}", Customer);
    }

    private void OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            ShowDialog = false;
        }

        ShowDialog = false;
    }
}