using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherAPI.Models;

public class WeatherDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("latitude")]
    public double Latitude { get; set; }

    [BsonElement("longitude")]
    public double Longitude { get; set; }

    [BsonElement("temperature")]
    public double Temperature { get; set; }

    [BsonElement("windDirection")]
    public double WindDirection { get; set; }

    [BsonElement("windSpeed")]
    public double WindSpeed { get; set; }

    [BsonElement("sunriseDateTime")]
    public string SunriseDateTime { get; set; } = string.Empty;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
