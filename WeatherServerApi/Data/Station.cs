using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherServerApi.Data
{
    public class Station
    {
        public long Id { get; set; }
        public Guid Api { get; set; }
        public String Name { get; set; }
        public virtual IdentityUser Owner { get; set; }
        public String Color { get; set; }
        public double Coordinate_X { get; set; }
        public double Coordinate_Y { get; set; }

    }
}
