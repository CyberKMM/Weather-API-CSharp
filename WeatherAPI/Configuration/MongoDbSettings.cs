namespace WeatherAPI.Configuration;
/// <summary>
/// Strongly-typed settings for the MongoDB connection.
/// </summary>
public class MongoDbSettings
{
    public string ConnectionString {get; set;} = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CollectionName { get; set; } = string.Empty;
}