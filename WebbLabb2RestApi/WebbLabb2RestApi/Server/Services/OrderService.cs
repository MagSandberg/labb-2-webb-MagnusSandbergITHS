using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task CreateOrder(OrderDto dto)
    {
        await _orderRepository.CreateOrder(dto);
    }
}