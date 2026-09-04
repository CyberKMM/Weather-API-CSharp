using WeatherAPI.Interfaces;
using WeatherAPI.Models;

namespace WeatherAPI.Services;

public class WeatherService: IWeatherService
{
    private readonly IWeatherRepository _repository;
    private readonly IOpenMeteoClient _client;
    private readonly ILogger<WeatherService> _logger;
    public WeatherService(IWeatherRepository repository, IOpenMeteoClient OpenMeteoclient, ILogger<WeatherService> logger)
    {
        _repository = repository;
        _client = OpenMeteoclient;
        _logger = logger;
    }

    public async Task<WeatherResult> GetWeatherByCoordinatesAsync(double latitude, double longitude)
    {
        var cached = await _repository.GetAsync(latitude, longitude);
        if (cached != null)
        {
            _logger.LogInformation("Cache hit for ({Latitude}, {Longitude})", latitude, longitude);
            return MapToResult(cached);
        }

        _logger.LogInformation("Cache miss for ({Latitude}, {Longitude}). Fetching from Open-Meteo.", latitude, longitude);

        OpenMeteoResponse response;
        try
        {
            response = await _client.GetWeatherAsync(latitude, longitude);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch weather data from Open-Meteo.");
            throw new ApplicationException("Unable to retrieve weather data from the external provider.");
        }

        var document = MapToDocument(latitude, longitude, response);
        await _repository.SaveAsync(document);

        return MapToResult(document);
    }

    private static WeatherResult MapToResult(WeatherDocument document) => new()
    {
        Temperature = document.Temperature,
        WindDirection = document.WindDirection,
        WindSpeed = document.WindSpeed,
        SunriseDateTime = document.SunriseDateTime
    };

    private static WeatherDocument MapToDocument(double latitude, double longitude, OpenMeteoResponse response)
    {
        var daily = response.Daily!;
        return new WeatherDocument
        {
            Latitude = latitude,
            Longitude = longitude,
            Temperature = daily.Temperature.FirstOrDefault() ?? 0,
            WindDirection = daily.WindDirection.FirstOrDefault() ?? 0,
            WindSpeed = daily.WindSpeed.FirstOrDefault() ?? 0,
            SunriseDateTime = daily.Sunrise.FirstOrDefault() ?? string.Empty
        };
    }
}