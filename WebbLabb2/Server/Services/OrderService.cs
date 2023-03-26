using WebbLabb2.DataAccess.Repositories;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> CreateOrder(OrderDto dto)
    {
        return await _orderRepository.CreateOrder(dto);
    }

    public async Task<bool> UpdateOrder(string id, OrderDto dto)
    {
        return await _orderRepository.UpdateOrder(id, dto);
    }

    public async Task<OrderDto> GetOrder(string id)
    {
        return await _orderRepository.GetOrder(id);
    }

    public async Task<OrderDto[]> GetOrders()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task<bool> RemoveOrder(string id)
    {
        return await _orderRepository.RemoveOrder(id);
    }
}