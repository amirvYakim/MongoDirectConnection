using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDirectConnection.Models;
using YakimGames.MongoDB.Connector;

namespace MongoDirectConnection.Controllers;

[ApiController]
[Route("[controller]")]

public class MongoController : ControllerBase
{
    private readonly IMongoConnectionManager _mongoConnectionManager;
    private readonly string _connectionString;
    
    public MongoController(IMongoConnectionManager mongoConnectionManager)
    {
        _mongoConnectionManager = mongoConnectionManager;
        _connectionString = GetConnectionString();
    }

    [HttpGet("GetAllPokemonTrainer")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var mongoDatabase = await _mongoConnectionManager.GetDatabaseAsync(_connectionString, "local");
            var collection = mongoDatabase.GetCollection<User>("test_collection");
            var filter = Builders<User>.Filter.Eq(u => u.IsPokemonTrainer, true);
            var document =  collection.Find(filter).ToList();
            
            return Ok(document);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }
    
    [HttpPost("InsertPokemonTrainer")]
    public async Task<IActionResult> Insert([FromBody] User user)
    {
        try
        {
            var mongoDatabase = await _mongoConnectionManager.GetDatabaseAsync(_connectionString,"local");
            var collection = mongoDatabase.GetCollection<User>("test_collection");
            await collection.InsertOneAsync(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }
    
    [HttpPut("UpdateSwitchStringIsTrainer")]
    public async Task<IActionResult> Update([FromBody] User user)
    {
        try
        {
            var mongoDatabase = await _mongoConnectionManager.GetDatabaseAsync(_connectionString,"local");
            var collection = mongoDatabase.GetCollection<User>("test_collection");
            var filter = Builders<User>.Filter.Eq(u => u.Name, "string");
            var update = Builders<User>.Update.Set(u => u.IsPokemonTrainer, !user.IsPokemonTrainer);
            await collection.UpdateOneAsync(filter, update);

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    private string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        if (connectionString == null)
        {
            throw new KeyNotFoundException();
        }

        return connectionString;
    }
}

