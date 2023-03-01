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

    public async Task UpdateOrder(string id, OrderDto dto)
    {
        await _orderRepository.UpdateOrder(id, dto);
    }

    public async Task<OrderDto[]> GetOrder(string id)
    {
        return await _orderRepository.GetOrder(id);
    }

    public async Task<OrderDto[]> GetOrders()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task RemoveOrder(string id)
    {
        await _orderRepository.RemoveOrder(id);
    }
}