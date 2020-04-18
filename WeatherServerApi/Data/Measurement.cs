using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherServerApi.Data
{
    public class Measurement
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Pressure { get; set; }
        public decimal RainSpeed { get; set; }
        public decimal WindSpeed { get; set; }
        public Station Station { get; set; }
    }
}
