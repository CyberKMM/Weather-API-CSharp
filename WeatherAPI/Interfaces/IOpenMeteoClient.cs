using WeatherAPI.Models;
namespace WeatherAPI.Interfaces;

public interface IOpenMeteoClient
{
    Task<OpenMeteoResponse> GetWeatherAsync(double latitude, double longitude);
}