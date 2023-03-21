using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductAdd : ComponentBase
{
    public ProductDto Product { get; set; } = new ();
    public async Task CreateProduct()
    {
        await PublicClient.Client.PostAsJsonAsync("createProduct", Product);
    }
}