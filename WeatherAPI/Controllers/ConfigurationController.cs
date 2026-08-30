using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherAPI.Configuration;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ConfigurationController : ControllerBase
{
    private readonly MongoDbSettings _mongosettings;

    public ConfigurationController(IOptions<MongoDbSettings> mongosettings)
    {
        _mongosettings = mongosettings.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {db = _mongosettings.DatabaseName});
    }
}
