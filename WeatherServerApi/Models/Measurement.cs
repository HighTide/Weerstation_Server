using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherServerApi.Models
{
    public class Measurement
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
    }
}
