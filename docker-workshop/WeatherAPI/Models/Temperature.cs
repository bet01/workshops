using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class Temperature
    {
        [Key]
        public Guid Id {get;set;}
        public DateTime DateTime { get; set; }
        public short Value { get; set; }
    }
}