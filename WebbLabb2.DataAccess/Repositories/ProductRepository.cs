using MongoDB.Driver;
using WebbLabb2.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.DataAccess.Repositories;

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

    public async Task<bool> CreateProduct(ProductDto dto)
    {
        if (await _productModelCollection.FindAsync(p => p.ProductName == dto.ProductName).Result.AnyAsync())
        {
            return false;
        }

        await _productModelCollection.InsertOneAsync(ConvertToModel(dto));
        return true;
    }

    public async Task<ProductDto> GetProductById(string id)
    {
        var product = await _productModelCollection
            .FindAsync(p => p.ProductId.Equals(id));

        var productIdExists = await _productModelCollection.FindAsync(o => o.ProductId.Equals(id)).Result.AnyAsync();

        return !productIdExists ? null! : ConvertToDto(product.FirstOrDefault());
    }

    public async Task<ProductDto> GetProductByName(string name)
    {
        var product = await _productModelCollection
            .FindAsync(p => p.ProductName.Equals(name));

        var productNameExists = await _productModelCollection.FindAsync(o => o.ProductName.Equals(name)).Result.AnyAsync();

        var emptyProduct = new ProductDto();

        emptyProduct.ProductName = "Not found";

        return !productNameExists ? emptyProduct : ConvertToDto(product.FirstOrDefault());
    }

    public async Task<ProductDto[]> GetAllProducts()
    {
        var products = await _productModelCollection.FindAsync(_ => true);

        return products.ToList().Select(ConvertToDto).ToArray();
    }

    public async Task<bool> UpdateProduct(string id, ProductDto dto)
    {
        var filter = Builders<ProductModel>.Filter.Eq("ProductId", id);
        var update = Builders<ProductModel>.Update
            .Set("ProductNumber", $"{dto.ProductNumber}")
            .Set("ProductName", $"{dto.ProductName}")
            .Set("ProductDescription", $"{dto.ProductDescription}")
            .Set("ProductPrice", $"{dto.ProductPrice}")
            .Set("ProductCategory", $"{dto.ProductCategory}")
            .Set("OrderCount", $"{dto.OrderCount}")
            .Set("ProductStatus", $"{dto.ProductStatus}")
            .Set("ProductImage", $"{dto.ProductImage}");

        var productIdExists = await _productModelCollection.Find(filter).AnyAsync();
        if (!productIdExists) return false;

        await _productModelCollection.UpdateOneAsync(filter, update);
        return true;
    }

    public async Task<bool> UpdateAvailability(string name, bool value)
    {
        var filter = Builders<ProductModel>.Filter.Eq("ProductName", name);
        var update = Builders<ProductModel>.Update
            .Set("ProductStatus", $"{value}");

        var productNameExists = await _productModelCollection.Find(filter).AnyAsync();
        if (!productNameExists) return false;

        await _productModelCollection.UpdateOneAsync(filter, update);
        return true;
    }

    public async Task<bool> RemoveProduct(string id)
    {
        var orderIdExists = await _productModelCollection.FindAsync(o => o.ProductId.Equals(id)).Result.AnyAsync();
        if (orderIdExists == false) return false;

        await _productModelCollection.DeleteOneAsync(p => p.ProductId.Equals(id));
        return true;
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
            OrderCount = dto.OrderCount,
            ProductStatus = dto.ProductStatus,
            ProductImage = dto.ProductImage
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
            OrderCount = dataModel.OrderCount,
            ProductStatus = dataModel.ProductStatus,
            ProductImage = dataModel.ProductImage
        };
    }
}