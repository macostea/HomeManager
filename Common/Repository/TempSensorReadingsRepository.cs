﻿using System;
using System.Collections.Generic;
using HomeManager.Common.Models;

namespace HomeManager.Common.Repository
{
    public class TempSensorReadingsRepository : IRepository<ISensorReading<double>>
    {
        private readonly IDBContext db;

        public TempSensorReadingsRepository(IDBContext db)
        {
            this.db = db;
        }

        public bool Add(ISensorReading<double> obj)
        {
            return this.db.AddTemperature(obj);
        }

        public List<ISensorReading<double>> GetList(string room, Predicate<ISensorReading<double>> filter)
        {

            var parameters = new Dictionary<string, string>
            {
                { "location", room }
            };
            var readings = this.db.QueryTemperature(parameters);

            return null == filter ? readings : readings.FindAll(filter);
        }
    }
}
