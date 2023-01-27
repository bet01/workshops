using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions options) : base(options) { }

        DbSet<Temperature> Temperatures
        {
            get;
            set;
        }
    }
}