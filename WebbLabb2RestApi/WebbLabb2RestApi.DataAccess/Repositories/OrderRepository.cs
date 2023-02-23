using MongoDB.Driver;
using WebbLabb2RestApi.DataAccess.Models;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.DataAccess.Repositories;

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

    public async Task<OrderDto[]> GetAllOrders()
    {
        var getAllOrders = await _orderModelCollection.FindAsync(_ => true);

        return getAllOrders.ToList().Select(ConvertToDto).ToArray();
    }

    private OrderModel ConvertToModel(OrderDto dto)
    {
        return new OrderModel()
        {
            CustomerEmail = dto.CustomerEmail,
            ProductList = dto.ProductList
        };
    }

    private OrderDto ConvertToDto(OrderModel dataModel)
    {
        return new OrderDto()
        {
            CustomerEmail = dataModel.CustomerEmail,
            ProductList = dataModel.ProductList,
        };
    }
}