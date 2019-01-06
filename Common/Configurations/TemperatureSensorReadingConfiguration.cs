using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configurations
{
    class TemperatureSensorReadingConfiguration : IEntityTypeConfiguration<TemperatureSensorReading>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TemperatureSensorReading> builder)
        {
            builder.HasKey(s => s.SensorReadingId);
            builder.Property(s => s.SensorReadingId).ValueGeneratedOnAdd();
        }
    }
}
