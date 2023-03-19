﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;
using WebbLabb2.Shared.Services;

namespace WebbLabb2.Client.Pages;

public partial class AdminCustomerEdit : ComponentBase
{
    public CustomerDto CurrentSelectedCustomer { get; set; } = new();
    public Guid CurrentCustomerId { get; set; } = new();
    public string Email { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Email = CurrentSelectedCustomer.Email;
        await GetCustomerByEmail(Email);

        await base.OnInitializedAsync();
    }

    public async Task GetCustomerByEmail(string email)
    {
        var response = new CustomerDto();

        if (string.IsNullOrEmpty(email))
        {
            CurrentSelectedCustomer = new CustomerDto();
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<CustomerDto>($"getCustomerByEmail/{Email}");
            CurrentSelectedCustomer = response!;
        }
    }

    public async Task UpdateCustomer()
    {
        CurrentCustomerId = CurrentSelectedCustomer.Id;

        var safetyDto = CurrentSelectedCustomer;

        if (CurrentSelectedCustomer.FirstName == string.Empty) { CurrentSelectedCustomer.FirstName = safetyDto.FirstName; }
        if (CurrentSelectedCustomer.LastName == string.Empty) { CurrentSelectedCustomer.LastName = safetyDto.LastName; }
        if (CurrentSelectedCustomer.Email == string.Empty) { CurrentSelectedCustomer.Email = safetyDto.Email; }
        if (CurrentSelectedCustomer.CellNumber == string.Empty) { CurrentSelectedCustomer.CellNumber = safetyDto.CellNumber; }
        if (CurrentSelectedCustomer.ZipCode == 0) { CurrentSelectedCustomer.ZipCode = safetyDto.ZipCode; }
        if (CurrentSelectedCustomer.City == string.Empty) { CurrentSelectedCustomer.City = safetyDto.City; }
        if (CurrentSelectedCustomer.StreetAddress == string.Empty) { CurrentSelectedCustomer.StreetAddress = safetyDto.StreetAddress; }

        await PublicClient.Client.PatchAsJsonAsync($"updateCustomer?id={CurrentCustomerId}", CurrentSelectedCustomer);
    }
}