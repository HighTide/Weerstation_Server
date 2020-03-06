﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherServerApi.Models
{
    public class Measurement
    {
        public long Id { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        
    }
}
