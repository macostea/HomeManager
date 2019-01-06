﻿using System;
using Domain.Entities;

namespace Common.Repository
{
    public class HumidityReadingWithSensorSpecification : BaseSpecification<HumiditySensorReading>
    {
        public int ReadingID { get; }

        public HumidityReadingWithSensorSpecification(int readingId) : base(b => b.SensorReadingId == readingId)
        {
            this.ReadingID = readingId;
            AddInclude(b => b.Sensor);
        }

        public HumidityReadingWithSensorSpecification() : base(null)
        {
            AddInclude(b => b.Sensor);
        }
    }
}
