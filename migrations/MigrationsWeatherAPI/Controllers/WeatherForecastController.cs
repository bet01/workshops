using Dapper;
using Microsoft.AspNetCore.Mvc;
using MigrationsWeatherAPI.Infrastructure;
using MigrationsWeatherAPI.Models;

namespace MigrationsWeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DapperContext _dapperContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DapperContext dapperContext)
    {
        _logger = logger;
        _dapperContext = dapperContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _dapperContext
        .CreateConnection()
        .Query<WeatherForecast>("SELECT * FROM WeatherForecast");
    }
}
