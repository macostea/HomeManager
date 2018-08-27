﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManagerWeb.Models
{
    public class SensorReadingFactory
    {
        public static ISensorReading<double> NewTemp(DbDataReader reader)
        {
            return new SensorReading<double>(reader.GetDateTime(0), reader.GetDouble(2));
        }
    }
}