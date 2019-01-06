using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configurations
{
    class HumiditySensorReadingConfiguration : IEntityTypeConfiguration<HumiditySensorReading>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HumiditySensorReading> builder)
        {
            builder.HasKey(s => s.SensorReadingId);
            builder.Property(s => s.SensorReadingId).ValueGeneratedOnAdd();
        }
    }
}
