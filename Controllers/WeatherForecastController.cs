using Cola.CoreUtils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KunLun.KlGetway.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var dt = _configuration.GetColaSection<Nacos1>("nacos1").DefaultTimeOut;
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                DefaultTimeOut = dt
            })
            .ToArray();
    }
}

public class Nacos1
{
    public long DefaultTimeOut { get; set; }
}