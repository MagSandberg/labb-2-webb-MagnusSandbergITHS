using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebbLabb2.DataAccess.Models;
using WebbLabb2.DataAccess.Sql.Contexts;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.DataAccess.Repositories;

public class OrderRepository
{
    private readonly IMongoCollection<OrderModel> _orderModelCollection;
    private readonly CustomerDbContext _customerDbContext;

    public OrderRepository(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;

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

    public async Task<bool> CreateOrder(OrderDto dto)
    {
        var validEmail = await _customerDbContext.CustomerModel.AnyAsync(c => c.Email == dto.CustomerEmail);
        if (validEmail == false) return false;

        await _orderModelCollection.InsertOneAsync(ConvertToModel(dto));
        return true;
    }

    public async Task CreatePlaceholderOrder(OrderDto dto)
    {
        await _orderModelCollection.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task<bool> UpdateOrder(string id, OrderDto dto)
    {
        var filter = Builders<OrderModel>.Filter.Eq("OrderId", id);
        var update = Builders<OrderModel>.Update
            .Set(o => o.ProductList, dto.ProductList);

        var orderIdExists = await _orderModelCollection.Find(filter).AnyAsync();
        if (!orderIdExists) return false;

        await _orderModelCollection.UpdateOneAsync(filter, update);
        return true;
    }

    public async Task UpdatePlaceholderOrder(string email, OrderDto dto)
    {
        var filter = Builders<OrderModel>.Filter.Eq("CustomerEmail", email);
        var update = Builders<OrderModel>.Update
            .Set(o => o.ProductList, dto.ProductList);

        await _orderModelCollection.UpdateOneAsync(filter, update);
    }

    public async Task<OrderDto> GetOrder(string id)
    {
        var order = await _orderModelCollection.FindAsync(o => o.OrderId.Equals(id));

        if (order != null) return ConvertToDto(await order.FirstOrDefaultAsync());

        var nullDto = new OrderDto
        {
            CustomerEmail = string.Empty,
            OrderId = "Not found",
            ProductList = new List<ProductDto>()
        };
        return nullDto;
    }

    public async Task<OrderDto> GetPlaceholderOrder(string email)
    {
        var order = await _orderModelCollection.FindAsync(o => o.CustomerEmail.Equals(email));

        if (order != null) return ConvertToDto(await order.FirstOrDefaultAsync());

        var nullDto = new OrderDto
        {
            CustomerEmail = "Not found",
            OrderId = "Not found",
            ProductList = new List<ProductDto>()
        };
        return nullDto;
    }

    public async Task<OrderDto[]> GetAllOrders()
    {
        var getAllOrders = await _orderModelCollection.FindAsync(_ => true);

        return getAllOrders.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task<bool> RemoveOrder(string id)
    {
        var orderIdExists = await _orderModelCollection.FindAsync(o => o.OrderId.Equals(id)).Result.AnyAsync();
        if (orderIdExists == false) return false;

        await _orderModelCollection.DeleteOneAsync(p => p.OrderId.Equals(id));
        return true;
    }

    public async Task<bool> RemovePlaceHolderOrder(string email)
    {
        var orderIdExists = await _orderModelCollection.FindAsync(o => o.CustomerEmail.Equals(email)).Result.AnyAsync();
        if (orderIdExists == false) return false;

        await _orderModelCollection.DeleteOneAsync(p => p.CustomerEmail.Equals(email));
        return true;
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