namespace WeatherAPI.Models;

/// <summary>
/// Represents the weather data returned to the API consumer.
/// </summary>
public class WeatherResult
{
    public double Temperature { get; set; }
    public double WindDirection { get; set; }
    public double WindSpeed { get; set; }
    public string SunriseDateTime { get; set; } = string.Empty;
}