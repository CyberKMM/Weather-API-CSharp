using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("200 ok");
    }
}