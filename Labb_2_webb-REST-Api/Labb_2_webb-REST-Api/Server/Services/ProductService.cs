using Labb_2_webb_REST_Api.DataAccess.Repositories;
using Labb_2_webb_REST_Api.Shared.DTOs;

namespace Labb_2_webb_REST_Api.Server.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task BroadcastMessage(ProductDto productDto)
    {
        await _productRepository.CreateProduct(productDto);
    }
}