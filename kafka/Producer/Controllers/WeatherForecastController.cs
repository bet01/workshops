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
    public async Task<List<string>> Get()
    {
        // Random rubbish data
        var weatherForecasts = Enumerable.Range(1, 100).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        // Kafka produce! - don't do this for production code, use singleton for producer!
        // Without singleton 1s+ to send, with singleton < 10ms
        using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
        {
            List<string> deliveryResults = new List<string>();

            foreach (var weatherForecast in weatherForecasts)
            {
                string json = JsonConvert.SerializeObject(weatherForecast);
                var message = new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = json };
                var deliveryResult = await producer.ProduceAsync("weather", message);
                deliveryResults.Add(deliveryResult.Status.ToString());
            }

            return deliveryResults;
        }
    }
}
