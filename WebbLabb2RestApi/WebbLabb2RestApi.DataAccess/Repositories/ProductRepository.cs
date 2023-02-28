using MongoDB.Driver;
using WebbLabb2RestApi.DataAccess.Models;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.DataAccess.Repositories;

public class ProductRepository
{
    private readonly IMongoCollection<ProductModel> _productModelCollection;

    public ProductRepository()
    {
        var host = "localhost";
        var databaseName = "ProductDb";
        var port = 27017;
        var connectionString = $"mongodb://{host}:{port}";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _productModelCollection =
            database.GetCollection<ProductModel>("Products",
                new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task CreateProduct(ProductDto dto)
    {
        await _productModelCollection.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task<ProductDto[]> GetAllProducts()
    {
        var products = await _productModelCollection.FindAsync(_ => true);

        return products.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task<ProductDto[]> GetProductByName(string name)
    {
        var product = await _productModelCollection
            .FindAsync(p => p.ProductName.Equals(name));

        return product.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task UpdateProduct(string id, ProductDto dto)
    {
        var filter = Builders<ProductModel>.Filter.Eq("ProductId", id);
        var update = Builders<ProductModel>.Update
            .Set("ProductNumber", $"{dto.ProductNumber}")
            .Set("ProductName", $"{dto.ProductName}")
            .Set("ProductDescription", $"{dto.ProductDescription}")
            .Set("ProductPrice", $"{dto.ProductPrice}")
            .Set("ProductCategory", $"{dto.ProductCategory}")
            .Set("ProductStatus", $"{dto.ProductStatus}");

        await _productModelCollection.UpdateOneAsync(filter, update);
    }

    public async Task UpdateAvailability(string name, bool value)
    {
        var filter = Builders<ProductModel>.Filter.Eq("ProductName", name);
        var update = Builders<ProductModel>.Update
            .Set("ProductStatus", $"{value}");

        await _productModelCollection.UpdateOneAsync(filter, update);
    }

    public async Task RemoveProduct(string name)
    {
        var product = await _productModelCollection
            .DeleteOneAsync(p => p.ProductName.Equals(name));
    }

    private ProductModel ConvertToModel(ProductDto dto)
    {
        return new ProductModel()
        {
            ProductId = dto.ProductId,
            ProductNumber = dto.ProductNumber,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductCategory = dto.ProductCategory,
            ProductStatus = dto.ProductStatus,
        };
    }

    private ProductDto ConvertToDto(ProductModel dataModel)
    {
        return new ProductDto()
        {
            ProductId = dataModel.ProductId,
            ProductNumber = dataModel.ProductNumber,
            ProductName = dataModel.ProductName,
            ProductDescription = dataModel.ProductDescription,
            ProductPrice = dataModel.ProductPrice,
            ProductCategory = dataModel.ProductCategory,
            ProductStatus = dataModel.ProductStatus,
        };
    }
}