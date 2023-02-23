using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebbLabb2RestApi.DataAccess.Models;

public class OrderModel
{
    [BsonId]
    public ObjectId OrderId { get; set; }
    [BsonElement]
    public string CustomerEmail { get; set; } = string.Empty;
    [BsonElement]
    public List<string> ProductList { get; set; } = new();
}