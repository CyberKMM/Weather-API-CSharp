using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherAPI.Configuration;
using WeatherAPI.Interfaces;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ConfigurationController : ControllerBase
{
    private readonly MongoDbSettings _mongosettings;
    private IOpenMeteoClient _client;

    public ConfigurationController(IOptions<MongoDbSettings> mongosettings, IOpenMeteoClient client)
    {
        _mongosettings = mongosettings.Value;
        _client = client;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {db = _mongosettings.DatabaseName});
    }

    [HttpGet("OpenMeteo")]
    public async Task<IActionResult> GetWeather()
    {
        var response = await _client.GetWeatherAsync(19.4326, -99.1332);

        return Ok(response);
    }
}
