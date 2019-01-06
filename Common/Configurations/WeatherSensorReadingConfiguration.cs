using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configurations
{
    class WeatherSensorReadingConfiguration : IEntityTypeConfiguration<WeatherSensorReading>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WeatherSensorReading> builder)
        {
            builder.HasKey(s => s.SensorReadingId);
            builder.Property(s => s.SensorReadingId).ValueGeneratedOnAdd();
            builder.Property(s => s.IconURL).HasConversion(v => v.ToString(), v => new Uri(v));
        }
    }
}
