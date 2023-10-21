using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController: ControllerBase {
    private static readonly Dictionary<string, int[]> Temperature = new Dictionary<string, int[]>() {
        { "Freezing", new[] { -25, -5 } },
        { "Bracing", new[] { -5, 5 } },
        { "Chilly", new[] { 5, 15 } },
        { "Cool", new[] { 16, 19 } },
        { "Balmy", new[] { 19, 25 } },
        { "Hot", new[] { 26, 30 } },
        { "Sweltering", new[] { 30, 39 } },
        { "Scorching", new[] { 40, 60 } }
    };

    private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
        return Enumerable.Range(1, 5).Select(index =>
        {
            var summary = Summaries[Random.Shared.Next(Summaries.Length)];
            var temp = Temperature[summary];
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(temp[0], temp[1]),
                Summary = summary
            };
        }).ToArray();
    }
}