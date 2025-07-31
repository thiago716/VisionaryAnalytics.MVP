using MongoDB.Driver;

namespace Infraestructure.Extensions;

public static class MongoDatabaseExtensions
{
    public static async Task CreateCollectionIfNotExistsAsync(this IMongoDatabase database, string collectionName)
    {
        var existingCollections = await database.ListCollectionNames().ToListAsync();
        if (!existingCollections.Contains(collectionName))
        {
            await database.CreateCollectionAsync(collectionName);
        }
    }
}
