using System;
using Common.Configurations;
using Domain.Entities;
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
            modelBuilder.ApplyConfiguration<Sensor>(new SensorConfiguration());
            modelBuilder.ApplyConfiguration<TemperatureSensorReading>(new TemperatureSensorReadingConfiguration());
            modelBuilder.ApplyConfiguration<HumiditySensorReading>(new HumiditySensorReadingConfiguration());
            modelBuilder.ApplyConfiguration<WeatherSensorReading>(new WeatherSensorReadingConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
