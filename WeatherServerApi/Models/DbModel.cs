using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherServerApi.Models
{
    public class DbModel : DbContext
    {
        public DbModel(DbContextOptions<DbModel> options)
            : base(options)
        {
        }

        public DbSet<Measurement> Measurements { get; set; }
    }
}
