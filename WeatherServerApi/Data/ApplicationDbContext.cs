using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeatherServerApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Station>()
        //        .Property(b => b.Color)
        //        .HasDefaultValueSql("#00B2FF");
        //}

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<Station> Stations { get; set; }
    }
}
