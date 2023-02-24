using MongoDB.Bson;
using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Services;

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

    public async Task<ProductDto[]> GetProductByName(string name)
    {
        return await _productRepository.GetProductByName(name);
    }

    public async Task<ProductDto[]> GetProducts()
    {
        return await _productRepository.GetAllProducts();
    }
}