using System.Text.Json.Serialization;

namespace WeatherAPI.Models;

public class OpenMeteoResponse
{
    [JsonPropertyName("daily")]
    public OpenMeteoDaily? Daily{get; set;}
}

public class OpenMeteoDaily
{
    [JsonPropertyName("temperature_2m_max")]
    public List<double?> Temperature { get; set; } = new();

    [JsonPropertyName("wind_direction_10m_dominant")]
    public List<double?> WindDirection { get; set; } = new();

    [JsonPropertyName("wind_speed_10m_max")]
    public List<double?> WindSpeed { get; set; } = new();

    [JsonPropertyName("sunrise")]
    public List<string?> Sunrise { get; set; } = new();
}