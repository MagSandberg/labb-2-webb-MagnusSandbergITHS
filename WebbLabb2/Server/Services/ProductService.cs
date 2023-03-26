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

    public async Task<bool> AddProduct(ProductDto productDto)
    {
        return await _productRepository.CreateProduct(productDto);
    }

    public async Task<ProductDto[]> GetProducts()
    {
        return await _productRepository.GetAllProducts();
    }

    public async Task<ProductDto> GetProductById(string id)
    {
        return await _productRepository.GetProductById(id);
    }

    public async Task<ProductDto> GetProductByName(string name)
    {
        return await _productRepository.GetProductByName(name);
    }

    public async Task<bool> UpdateProduct(string id, ProductDto dto)
    {
        return await _productRepository.UpdateProduct(id, dto);
    }

    public async Task<bool> RemoveProduct(string id)
    {
        return await _productRepository.RemoveProduct(id);
    }

    public async Task<bool> UpdateAvailability(string name, bool value)
    {
        return await _productRepository.UpdateAvailability(name, value);
    }
}