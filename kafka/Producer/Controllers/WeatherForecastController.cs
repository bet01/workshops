using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // Kafka stuff

    ProducerConfig _producerConfig = new ProducerConfig
    {
        BootstrapServers = "127.0.0.1:29092"
    };

    // End of Kafka stuff


    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SendWeatherForecast")]
    public async Task<string> Get()
    {
        // Random rubbish data
        var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        // Kafka produce!
        using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
        {
            string json = JsonConvert.SerializeObject(weather);
            var message = new Message<string, string> { Key = DateTime.Now.ToString("yyyyMMddHHmmss"), Value = json };

            var deliveryResult = await producer.ProduceAsync("weather", message);
            return deliveryResult.Status.ToString();
        }
    }
}
