using FluentMigrator;
using MigrationsWeatherAPI.Models;

namespace MigrationsWeatherAPI;

[Migration(202402090702)]
public class Migration_202402090702_InitialSeed : Migration
{
    public override void Up()
    {
        string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        foreach (var weatherForecast in weatherForecasts)
        {
            Insert.IntoTable("WeatherForecast").Row(new
            {
                Date = "2024-02-09",
                Summary = "Hot",
                TemperatureC = 30,
                TemperatureF = 86
            });
        }
    }

    public override void Down()
    {
        Delete.FromTable("WeatherForecast").AllRows();
    }
}