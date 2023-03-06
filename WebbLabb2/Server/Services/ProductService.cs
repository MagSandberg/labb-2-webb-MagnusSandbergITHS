using WebbLabb2.DataAccess.Repositories;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task AddProduct(ProductDto productDto)
    {
        await _productRepository.CreateProduct(productDto);
    }

    public async Task UpdateProduct(string id, ProductDto dto)
    {
        await _productRepository.UpdateProduct(id, dto);
    }

    public async Task<ProductDto[]> GetProductByName(string name)
    {
        return await _productRepository.GetProductByName(name);
    }

    public async Task<ProductDto[]> GetProducts()
    {
        return await _productRepository.GetAllProducts();
    }

    public async Task RemoveProduct(string name)
    {
        await _productRepository.RemoveProduct(name);
    }

    public async Task UpdateAvailability(string name, bool value)
    {
        await _productRepository.UpdateAvailability(name, value);
    }
}