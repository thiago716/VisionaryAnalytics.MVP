
using Core.Entity;
using Infraestructure.Extensions;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infraestructure.Configuration;

public class DataContext
{
     private readonly IMongoDatabase _database;
     public IMongoCollection<ProcessedVideo> ProcessedVideos { get; }

     public DataContext(IOptions<MongoDbSettings> settings)
     {
          var client = new MongoClient(settings.Value.ConnectionString);
          _database = client.GetDatabase(settings.Value.Database);
          
          _database.CreateCollectionIfNotExistsAsync(settings.Value.Collection).GetAwaiter().GetResult();

          ProcessedVideos = _database.GetCollection<ProcessedVideo>(settings.Value.Collection);
     }
}
