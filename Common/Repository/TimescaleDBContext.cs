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
            return this.Add("temperature", new Dictionary<string, object>
            {
                { "time", reading.Time },
                { "reading", reading.Reading },
                { "sensor", 1}
            });
        }

        public bool AddHumidity(ISensorReading<double> reading)
        {
            return this.Add("humidity", new Dictionary<string, object>
            {
                { "time", reading.Time },
                { "reading", reading.Reading },
                { "sensor", 1}
            });
        }

        public bool AddSensor(ISensor sensor)
        {
            return this.Add("sensor", new Dictionary<string, object>
            {
                { "name", sensor.Name },
                { "location", sensor.Location },
                { "type", sensor.Type }
            });
        }

        public IEnumerable<ISensorReading<double>> QueryTemperature(Dictionary<string, object> queryParameters) => this.Query<SensorReading<double>>("temperature", queryParameters);
        public IEnumerable<ISensorReading<double>> QueryHumidity(Dictionary<string, object> queryParameters) => this.Query<SensorReading<double>>("humidity", queryParameters);
        public IEnumerable<ISensor> QuerySensors(Dictionary<string, object> queryParameters) => this.Query<Sensor>("sensor", queryParameters);

        private bool Add(string table, Dictionary<string, object> values)
        {
            var rows = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    var atKeys = new List<string>();

                    foreach (var val in values)
                    {
                        atKeys.Add($"@{val.Key}");
                        cmd.Parameters.AddWithValue($"@{val.Key}", val.Value);
                    }

                    values.Keys.ToList().ForEach((obj) => atKeys.Add($"@{obj}"));

                    cmd.CommandText = $"INSERT INTO {table}({string.Join(", ", values.Keys)}) VALUES ({string.Join(", ", atKeys)})";
                    rows = cmd.ExecuteNonQuery();
                }
            }

            return rows != 0;
        }

        private IEnumerable<T> Query<T>(string table, Dictionary<string, object> queryParameters) where T : ITimescaleRepresentable, new()
        {
            var readings = new List<T>();

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    var atKeys = new List<string>();
                    var conditions = new List<string>();
                    foreach (var entry in queryParameters)
                    {
                        atKeys.Add($"@{entry.Key}");
                        conditions.Add($"{entry.Key} = @{entry.Key}");
                        cmd.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
                    }

                    cmd.CommandText = $"SELECT * FROM {table} WHERE {string.Join(" AND ", conditions)}";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var obj = new T();
                            obj.FromTimescaleReader(reader);

                            readings.Add(obj);
                        }
                    }
                }
            }

            return readings;
        }
    }
}
