﻿using MongoDB.Driver;
using RESTApi.DataAccess.Models;
using RESTApi.Shared.DTOs;

namespace RESTApi.DataAccess.Repositories;

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

    private ProductModel ConvertToModel(ProductDto dto)
    {
        return new ProductModel()
        {
            ProductNumber = dto.ProductNumber,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductCategory = dto.ProductCategory,
            ProductStatus = dto.ProductStatus,
        };
    }

    public async Task CreateProduct(ProductDto dto)
    {
        await _productModelCollection.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task<ProductDto[]> GetAllProducts()
    {
        var getAllProducts = await _productModelCollection.FindAsync(_ => true);

        return getAllProducts.ToList().Select(ConvertToDto).ToArray();
    }

    private ProductDto ConvertToDto(ProductModel dataModel)
    {
        return new ProductDto()
        {
            ProductNumber = dataModel.ProductNumber,
            ProductName = dataModel.ProductName,
            ProductDescription = dataModel.ProductDescription,
            ProductPrice = dataModel.ProductPrice,
            ProductCategory = dataModel.ProductCategory,
            ProductStatus = dataModel.ProductStatus,
        };
    }
}