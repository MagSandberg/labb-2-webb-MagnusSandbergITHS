using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebbLabb2.DataAccess.Models;

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
    public float ProductPrice { get; set; }
    [BsonElement]
    public string ProductCategory { get; set; } = string.Empty;
    [BsonElement]
    public int OrderCount { get; set; }
    [BsonElement]
    public bool ProductStatus { get; set; }
    [BsonElement]
    public string ProductImage { get; set; } = string.Empty;
}