using WeatherAPI.Models;

namespace WeatherAPI.Interfaces;

public interface IWeatherService
{
    public Task<WeatherResult> GetWeatherByCoordinatesAsync(double latitude, double longitude);
}