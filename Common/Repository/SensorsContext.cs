using System;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Repository
{
    public class SensorsContext : DbContext
    {
        public SensorsContext(DbContextOptions<SensorsContext> options) : base(options) { }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<TemperatureSensorReading> TemperatureSensorReadings { get; set; }
        public DbSet<HumiditySensorReading> HumiditySensorReadings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasPostgresExtension("timescaledb");
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
