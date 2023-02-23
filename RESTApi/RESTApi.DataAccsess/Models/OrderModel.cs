using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RESTApi.DataAccess.Models;

public class OrderModel
{
    [BsonId]
    public ObjectId OrderId { get; set; }
    [BsonElement]
    public string CustomerEmail { get; set; } = string.Empty;
    [BsonElement]
    public List<string> ProductList { get; set; } = new();
}