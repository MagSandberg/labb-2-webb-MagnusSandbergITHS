using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebbutvecklingLabb2RESTApi.DataAccess.Models;

public class CustomerModel
{
    [BsonId]
    public ObjectId CustomerId { get; set; }
    [BsonElement]
    public string FirstName { get; set; } = string.Empty;
    [BsonElement]
    public string LastName { get; set; } = string.Empty;
    [BsonElement]
    public string Email { get; set; } = string.Empty;
    [BsonElement]
    public int CellNumber { get; set; }
    [BsonElement]
    public string StreetAddress { get; set; } = string.Empty;
    [BsonElement]
    public string City { get; set; } = string.Empty;
    [BsonElement]
    public int ZipCode { get; set; }
}