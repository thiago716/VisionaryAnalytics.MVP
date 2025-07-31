
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
          if (settings?.Value == null)
            throw new ArgumentNullException(nameof(settings), "Configuração do MongoDB não fornecida.");

          if (string.IsNullOrWhiteSpace(settings.Value.ConnectionString))
            throw new ArgumentException("ConnectionString do MongoDB não está configurada.");
          if (string.IsNullOrWhiteSpace(settings.Value.Database))
            throw new ArgumentException("Database do MongoDB não está configurada.");
          if (string.IsNullOrWhiteSpace(settings.Value.Collection))
            throw new ArgumentException("Collection do MongoDB não está configurada.");

          var client = new MongoClient(settings.Value.ConnectionString);
          _database = client.GetDatabase(settings.Value.Database);
          
          _database.CreateCollectionIfNotExistsAsync(settings.Value.Collection).GetAwaiter().GetResult();

          ProcessedVideos = _database.GetCollection<ProcessedVideo>(settings.Value.Collection);
     }
}
