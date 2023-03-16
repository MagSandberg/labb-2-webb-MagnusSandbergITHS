using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductsAll : ComponentBase
{
    public List<ProductDto> AllProducts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AllProducts = new List<ProductDto>();

        var response = await PublicClient.Client.GetFromJsonAsync<ProductDto[]>("getAllProducts");

        if (response != null)
        {
            AllProducts.AddRange(response);
        }

        await base.OnInitializedAsync();
    }
}