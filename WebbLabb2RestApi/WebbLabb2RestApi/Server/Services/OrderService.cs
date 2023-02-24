using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Services;

public class OrderService
{
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public OrderService(ProductRepository productRepository, OrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ProductDto[]> AddProductToList(string name)
    {
        return await _productRepository.GetProductByName(name);
    }
}