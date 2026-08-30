using WeatherAPI.Models;
using MongoDB.Driver;
using WeatherAPI.Configuration;
using Microsoft.Extensions.Options;
using WeatherAPI.Interfaces;

namespace WeatherAPI.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly IMongoCollection<WeatherDocument> _collection;

    public WeatherRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<WeatherDocument>(settings.Value.CollectionName);

        EnsureIndexes();
    }

    /// <inheritdoc/>
    public async Task<WeatherDocument?> GetAsync(double latitude, double longitude)
    {
        var filter = Builders<WeatherDocument>.Filter.And(
            Builders<WeatherDocument>.Filter.Eq(d => d.Latitude, latitude),
            Builders<WeatherDocument>.Filter.Eq(d => d.Longitude, longitude)
        );

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task SaveAsync(WeatherDocument document)
    {
        document.CreatedAt = DateTime.UtcNow;
        await _collection.InsertOneAsync(document);
    }

    /// <summary>
    /// Creates a compound index on latitude and longitude for efficient cache lookups.
    /// </summary>
    private void EnsureIndexes()
    {
        var indexKeys = Builders<WeatherDocument>.IndexKeys
            .Ascending(d => d.Latitude)
            .Ascending(d => d.Longitude);

        var indexModel = new CreateIndexModel<WeatherDocument>(indexKeys);
        _collection.Indexes.CreateOne(indexModel);
    }
}
