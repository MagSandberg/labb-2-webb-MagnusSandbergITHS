using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages.AdminPages;

public partial class AdminProductAdd : ComponentBase
{
    public ProductDto Product { get; set; } = new();
    private bool ShowDialog { get; set; }
    public async Task CreateProduct()
    {
        SetDefaultValuesProduct();

        await PublicClient.Client.PostAsJsonAsync("createProduct", Product);
    }

    private void SetDefaultValuesProduct()
    {
        if (Product.ProductImage.IsNullOrEmpty())
        {
            Product.ProductImage = "resources/images/noimage.png";
        }

        if (Product.ProductCategory.IsNullOrEmpty())
        {
            Product.ProductCategory = "Uncategorized";
        }
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