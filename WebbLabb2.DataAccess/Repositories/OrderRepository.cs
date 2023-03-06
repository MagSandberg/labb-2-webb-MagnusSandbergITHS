using MongoDB.Driver;
using WebbLabb2.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.DataAccess.Repositories;

public class OrderRepository
{
    private readonly IMongoCollection<OrderModel> _orderModelCollection;

    public OrderRepository()
    {
        var host = "localhost";
        var databaseName = "OrderDb";
        var port = 27017;
        var connectionString = $"mongodb://{host}:{port}";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _orderModelCollection =
            database.GetCollection<OrderModel>("Orders",
                new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task CreateOrder(OrderDto dto)
    {
        await _orderModelCollection.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task UpdateOrder(string id, OrderDto dto)
    {
        var filter = Builders<OrderModel>.Filter.Eq("OrderId", id);
        var update = Builders<OrderModel>.Update
            .Set(o => o.ProductList, dto.ProductList);

        await _orderModelCollection.UpdateOneAsync(filter, update);
    }

    public async Task<OrderDto[]> GetOrder(string id)
    {
        var order = await _orderModelCollection.FindAsync(o => o.OrderId.Equals(id));
        return order.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task<OrderDto[]> GetAllOrders()
    {
        var getAllOrders = await _orderModelCollection.FindAsync(_ => true);

        return getAllOrders.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task RemoveOrder(string id)
    {
        await _orderModelCollection.DeleteOneAsync(p => p.OrderId.Equals(id));
    }

    private OrderModel ConvertToModel(OrderDto dto)
    {
        return new OrderModel()
        {
            OrderId = dto.OrderId,
            CustomerEmail = dto.CustomerEmail,
            ProductList = dto.ProductList
        };
    }

    private OrderDto ConvertToDto(OrderModel dataModel)
    {
        return new OrderDto()
        {
            OrderId = dataModel.OrderId,
            CustomerEmail = dataModel.CustomerEmail,
            ProductList = dataModel.ProductList
        };
    }
}