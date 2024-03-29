using MongoDirectConnection.Models;
using YakimGames.MongoDB.Connector.MongoConnection;
using YakimGames.MongoDB.Connector.MongoQuery;

namespace MongoDirectConnection.Handlers;

public class AddPokemonTrainerHandler : MongoQueryHandlerBase<User>
{
    private readonly IMongoConnectionManager _mongoConnectionManager;
    private readonly ILoggerFactory _loggerFactory;
    public AddPokemonTrainerHandler(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _mongoConnectionManager = new MongoConnectionManager(loggerFactory);
    }
    protected override async Task MongoOnExecuteAsync(User user)
    {
        var mongoDatabase = await _mongoConnectionManager.GetDatabaseAsync("mongodb://localhost:27017","local");
        var collection = mongoDatabase.GetCollection<User>("test_collection");
        await collection.InsertOneAsync(user);
    }
}