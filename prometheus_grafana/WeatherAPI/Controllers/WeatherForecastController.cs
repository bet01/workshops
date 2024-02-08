using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // Increment the WeatherRequestCount counter
        AppMetrics.WeatherRequestCount.Inc();

        // Measure the duration of the request for the CallDuration histogram
        using (AppMetrics.CallDuration.NewTimer())
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            // Sleep between 0 and 10 seconds to simulate a long running request
            Thread.Sleep(Random.Shared.Next(0, 10_000));

            // Add the duration of the request to the LastRequestDuration gauge
            AppMetrics.LastRequestDuration.Set(stopWatch.ElapsedMilliseconds);

            return data;
        }
    }
}
