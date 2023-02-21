using WebbutvecklingLabb2RESTApi.DataAccess.Repositories;
using WebbutvecklingLabb2RESTApi.Shared.DTOs;

namespace WebbutvecklingLabb2RESTApi.DataAccess.Services;

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
}