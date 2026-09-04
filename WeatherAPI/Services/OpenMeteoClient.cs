using System.Text.Json;
using WeatherAPI.Interfaces;
using WeatherAPI.Models;

namespace WeatherAPI.Services;

public class OpenMeteoClient : IOpenMeteoClient
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public OpenMeteoClient(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<OpenMeteoResponse> GetWeatherAsync(double latitude, double longitude)
    {
        var url = $"forecast?latitude={latitude}&longitude={longitude}" +
                  "&daily=temperature_2m_max,wind_direction_10m_dominant,wind_speed_10m_max,sunrise" +
                  "&timezone=auto&forecast_days=1";   

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenMeteoResponse>(content, JsonOptions);

        if (result?.Daily == null)
            throw new InvalidOperationException("Open-Meteo returned an unexpected response format.");

        return result;
    }
}