using WeatherAPI.Models;

namespace WeatherAPI.Interfaces;

public interface IWeatherRepository
{
    Task<WeatherDocument?> GetAsync(double lattitude, double longitude);
    Task SaveAsync(WeatherDocument document);
}