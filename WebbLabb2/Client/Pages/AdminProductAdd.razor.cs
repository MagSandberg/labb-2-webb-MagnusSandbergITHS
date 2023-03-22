using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductAdd : ComponentBase
{
    public ProductDto Product { get; set; } = new ();
    public async Task CreateProduct()
    {
        if (Product.ProductImage.IsNullOrEmpty()) 
        { Product.ProductImage = "resources/images/noimage.png"; }

        await PublicClient.Client.PostAsJsonAsync("createProduct", Product);
    }
}