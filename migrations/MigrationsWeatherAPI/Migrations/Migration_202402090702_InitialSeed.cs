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
            Date = DateTime.Today.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        foreach (var weatherForecast in weatherForecasts)
        {
            Insert.IntoTable("WeatherForecast").Row(weatherForecast);
        }
    }

    public override void Down()
    {
        Delete.FromTable("WeatherForecast").AllRows();
    }
}