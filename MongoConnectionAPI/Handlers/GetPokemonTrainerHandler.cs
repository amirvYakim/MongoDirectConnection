using MongoDB.Driver;
using MongoDirectConnection.Models;
using YakimGames.MongoDB.Connector;

namespace MongoDirectConnection.Handlers;

public class GetPokemonTrainerHandler : MongoQueryLinkBase<NoArgument>
{
    private readonly IMongoConnectionManager _mongoConnectionManager;
    private readonly ILoggerFactory _loggerFactory;
    public GetPokemonTrainerHandler(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _mongoConnectionManager = new MongoConnectionManager(loggerFactory);
    }
    
    protected override async Task MongoOnExecuteAsync(NoArgument noArgument)
    {
        var mongoDatabase = await _mongoConnectionManager.GetDatabaseAsync("mongodb://localhost:27017", "local");
        var collection = mongoDatabase.GetCollection<User>("test_collection");
        var filter = Builders<User>.Filter.Eq(u => u.IsPokemonTrainer, true);
        var document =  collection.Find(filter).ToList();
        SetResultData(document);
    }
}