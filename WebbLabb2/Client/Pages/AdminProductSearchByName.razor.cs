using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductSearchByName : ComponentBase
{
    public ProductDto CurrentProduct { get; set; } = new();
    public string ProductNameSearch { get; set; } = string.Empty;
    private string CurrentProductId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await GetProductByName();

        await base.OnInitializedAsync();
    }

    public async Task GetProductByName()
    {
        var response = new ProductDto();

        if (string.IsNullOrEmpty(ProductNameSearch))
        {
            ProductNameSearch = "Please enter a name";
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<ProductDto>($"getProductByName/{ProductNameSearch}");
            if (response == null)
            {
                CurrentProduct.ProductName = "Not found";
            }
            CurrentProduct = response;
        }
    }

    public async Task UpdateProduct()
    {
        CurrentProductId = CurrentProduct.ProductId;

        var safetyDto = CurrentProduct;

        if (CurrentProduct.ProductNumber == 0) { CurrentProduct.ProductNumber = safetyDto.ProductNumber; }
        if (CurrentProduct.ProductName == string.Empty) { CurrentProduct.ProductName = safetyDto.ProductName; }
        if (CurrentProduct.ProductDescription == string.Empty) { CurrentProduct.ProductDescription = safetyDto.ProductDescription; }
        if (CurrentProduct.ProductPrice == 0) { CurrentProduct.ProductPrice = safetyDto.ProductPrice; }
        if (CurrentProduct.ProductCategory == string.Empty) { CurrentProduct.ProductCategory = safetyDto.ProductCategory; }
        if (CurrentProduct.ProductImage == string.Empty) { CurrentProduct.ProductImage = "resources/images/noimage.png"; }

        await PublicClient.Client.PatchAsJsonAsync($"updateProduct?id={CurrentProductId}", CurrentProduct);
    }
}