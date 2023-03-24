﻿using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.CustomerPages;

public partial class CustomerCart : ComponentBase
{
    public Index SetPlaceholder = new();
    public OrderDto Order { get; set; } = new();
    public string CurrentOrderEmail { get; set; } = string.Empty;
    public string CurrentOrderId { get; set; } = string.Empty;
    private bool ShowDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentOrderEmail = SetPlaceholder.PlaceholderEmail;

        await GetPlaceholderOrder();

        await base.OnInitializedAsync();
    }

    //TODO Lägg till get order placeholderorder
    public async Task GetPlaceholderOrder()
    {
        var result = await PublicClient.Client.GetFromJsonAsync<OrderDto>($"getPlaceholderOrder/{CurrentOrderEmail}");

        if (result != null)
        {
            CurrentOrderId = result.OrderId;
        }
    }

    public void RemoveProductAtIndex(string productName)
    {
        var filter = Order.ProductList!.FindIndex(p => p.ProductName == productName);
        Order.ProductList.RemoveAt(filter);
    }

    public async Task UpdatePlaceholderOrder()
    {
        await PublicClient.Client.PatchAsJsonAsync($"updatePlaceholderOrder?email={CurrentOrderEmail}", Order);
    }

    private async Task OnConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            await PublicClient.Client.DeleteAsync($"removeOrder/{CurrentOrderId}");
        }

        ShowDialog = false;
    }

    private void Close(bool confirmed)
    {
        ShowDialog = false;
    }
}