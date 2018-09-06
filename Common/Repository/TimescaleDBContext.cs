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
        private readonly string connString = "Host=172.23.0.2;Port=9009;Username=postgres;Password=password;Database=sensors";

        public bool AddTemperature(ISensorReading<double> reading)
        {
            var rows = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO temperature(time, reading, sensor) VALUES (@time, @reading, 1)";
                    cmd.Parameters.AddWithValue("@reading", reading.Reading);
                    cmd.Parameters.AddWithValue("@time", reading.Time);

                    rows = cmd.ExecuteNonQuery();
                }
            }

            return rows != 0;
        }

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
