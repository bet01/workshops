using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    [Route("[controller]")]
    public class AddTemperatureController : Controller
    {
        private readonly ILogger<AddTemperatureController> _logger;
        private readonly WeatherContext _weatherContext;

        public AddTemperatureController(ILogger<AddTemperatureController> logger, WeatherContext weatherContext)
        {
            _logger = logger;
            _weatherContext = weatherContext;
        }

        [HttpGet()]
        public void Add()
        {
            Random random = new Random();

            Temperature temperature = new Temperature
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                Value = (short)random.Next(-30, 30)
            };

            _weatherContext.Add(temperature);
            _weatherContext.SaveChanges();
        }
    }
}