﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebbLabb2.Client.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Pages;

public partial class AdminProductEdit : ComponentBase
{
    public ProductDto CurrentSelectedProduct { get; set; } = new();
    public string CurrentName { get; set; } = string.Empty;
    public string CurrentProductId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        CurrentName = CurrentProductService.CurrentName;
        await GetProductByName(CurrentName);

        await base.OnInitializedAsync();
    }

    public async Task GetProductByName(string name)
    {
        var response = new ProductDto();

        if (string.IsNullOrEmpty(name))
        {
            CurrentSelectedProduct = new ProductDto();
        }
        else
        {
            response = await PublicClient.Client.GetFromJsonAsync<ProductDto>($"getProductByName/{CurrentName}");
            CurrentSelectedProduct = response!;
        }
    }

    public async Task UpdateProduct()
    {
        CurrentProductId = CurrentSelectedProduct.ProductId;
        var safetyDto = CurrentSelectedProduct;

        if (CurrentSelectedProduct.ProductName == string.Empty) { CurrentSelectedProduct.ProductName = safetyDto.ProductName; }
        if (CurrentSelectedProduct.ProductDescription == string.Empty) { CurrentSelectedProduct.ProductDescription = safetyDto.ProductDescription; }
        if (CurrentSelectedProduct.ProductCategory == string.Empty) { CurrentSelectedProduct.ProductCategory = safetyDto.ProductCategory; }
        if (CurrentSelectedProduct.ProductNumber == 0) { CurrentSelectedProduct.ProductNumber = safetyDto.ProductNumber; }
        if (CurrentSelectedProduct.ProductImage == string.Empty) { CurrentSelectedProduct.ProductImage = safetyDto.ProductImage; }

        await PublicClient.Client.PatchAsJsonAsync($"updateProduct?id={CurrentProductId}", CurrentSelectedProduct);
    }
}