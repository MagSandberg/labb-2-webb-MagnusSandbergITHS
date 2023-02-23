using MongoDB.Driver;
using WebbLabb2RestApi.DataAccess.Models;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.DataAccess.Repositories;

public class CustomerRepository
{
    private readonly IMongoCollection<CustomerModel> _customerModelCollection;

    public CustomerRepository()
    {
        var host = "localhost";
        var databaseName = "CustomerDb";
        var port = 27017;
        var connectionString = $"mongodb://{host}:{port}";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _customerModelCollection =
            database.GetCollection<CustomerModel>("Customers",
                new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task CreateCustomer(CustomerDto dto)
    {
        await _customerModelCollection.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task<CustomerDto[]> GetAllCustomers()
    {
        var getAllCustomers = await _customerModelCollection.FindAsync(_ => true);

        return getAllCustomers.ToList().Select(ConvertToDto).ToArray();
    }

    private CustomerModel ConvertToModel(CustomerDto dto)
    {
        return new CustomerModel()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            CellNumber = dto.CellNumber,
            StreetAddress = dto.StreetAddress,
            City = dto.City,
            ZipCode = dto.ZipCode
        };
    }

    private CustomerDto ConvertToDto(CustomerModel dataModel)
    {
        return new CustomerDto()
        {
            FirstName = dataModel.FirstName,
            LastName = dataModel.LastName,
            Email = dataModel.Email,
            CellNumber = dataModel.CellNumber,
            StreetAddress = dataModel.StreetAddress,
            City = dataModel.City,
            ZipCode = dataModel.ZipCode
        };
    }
}