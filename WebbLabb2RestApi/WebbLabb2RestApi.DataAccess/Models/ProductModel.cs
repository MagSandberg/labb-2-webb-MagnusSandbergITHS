using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebbLabb2RestApi.DataAccess.Models;

public class ProductModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; } = string.Empty;
    [BsonElement]
    public int ProductNumber { get; set; }
    [BsonElement]
    public string ProductName { get; set; } = string.Empty;
    [BsonElement]
    public string ProductDescription { get; set; } = string.Empty;
    [BsonElement]
    public int ProductPrice { get; set; }
    [BsonElement]
    public string ProductCategory { get; set; } = string.Empty;
    [BsonElement]
    public bool ProductStatus { get; set; }
}