using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.DataAccess.Models;

public class OrderModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderId { get; set; } = string.Empty;
    [BsonElement]
    public string CustomerEmail { get; set; } = string.Empty;
    [BsonElement]
    public List<ProductDto>? ProductList { get; set; }
}