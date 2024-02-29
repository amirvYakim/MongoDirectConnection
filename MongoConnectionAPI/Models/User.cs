using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDirectConnection.Models;

public class User
{
    public string Name { get; set; }
    public string Pokemon { get; set; }
    public bool IsPokemonTrainer { get; set; }
    
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
}