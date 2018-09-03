using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Common.Models;
using Npgsql;

namespace HomeManager.Common.Repository
{
    public class TimescaleDBContext : IDBContext
    {
        private readonly string connString = "Host=centos-local.mcostea.com;Username=postgres;Password=password;Database=sensors";

        public List<ISensorReading<double>> QueryTemperature(Dictionary<string, string> queryParameters)
        {
            var readings = new List<ISensorReading<double>>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM temperature where ";
                    foreach (var entry in queryParameters)
                    {
                        cmd.CommandText += $"{entry.Key} = @{entry.Key} AND ";
                        cmd.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
                    }
                    cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 5); // Remove trailing AND (there should be a better way to do this)

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
