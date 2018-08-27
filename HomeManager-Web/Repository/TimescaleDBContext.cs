using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HomeManagerWeb.Models;
using Npgsql;

namespace HomeManagerWeb.Repository
{
    public class TimescaleDBContext : IDBContext
    {
        private readonly string connString = "Host=centos-local.mcostea.com;Username=postgres;Password=password;Database=sensors";

        public List<ISensorReading<double>> QueryTemperature(string query)
        {
            var readings = new List<ISensorReading<double>>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readings.Add(SensorReadingFactory.NewTemp(reader));
                        }
                    }
                }
            }

            return readings;
        }
    }
}
