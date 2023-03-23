using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Client.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductsAll : ComponentBase
{
    public List<ProductDto> AllProducts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAllProductsAndPopulateList();

        await base.OnInitializedAsync();
    }

    private async Task GetAllProductsAndPopulateList()
    {
        AllProducts = new List<ProductDto>();

        var response = await PublicClient.Client.GetFromJsonAsync<ProductDto[]>("getAllProducts");

        if (response != null)
        {
            AllProducts.AddRange(response);
        }
    }

    private async Task UpdateAvailabilityStatus(string name, bool status)
    {
       await PublicClient.Client.PatchAsJsonAsync($"updateAvailability?name={name}&value={status}", AllProducts);
    }

    public void SetCurrentProductName(string name)
    {
        CurrentProductService.CurrentName = name;
    }
}