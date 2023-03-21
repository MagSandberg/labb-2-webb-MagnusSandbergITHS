using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class CustomerRegistration : ComponentBase
{
    public CustomerDto Customer { get; set; } = new();
    public string Email { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var userAuth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        Email = userAuth.User.Identity.Name;
        Customer.Email = Email;

        await base.OnInitializedAsync();
    }

    public async Task CreateCustomer()
    {
        await PublicClient.Client.PostAsJsonAsync("createCustomer", Customer);
    }
}