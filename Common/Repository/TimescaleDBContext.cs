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

        public TimescaleDBContext(String connString)
        {
            this.connString = connString;
        }

        public async Task<bool> AddTemperature(ISensorReading<double> reading)
        {
            return await this.Add("temperature", new Dictionary<string, object>
            {
                { "time", reading.Time },
                { "reading", reading.Reading },
                { "sensor", 1}
            });
        }

        public async Task<bool> AddHumidity(ISensorReading<double> reading)
        {
            return await this.Add("humidity", new Dictionary<string, object>
            {
                { "time", reading.Time },
                { "reading", reading.Reading },
                { "sensor", 1}
            });
        }

        public async Task<bool> AddSensor(ISensor sensor)
        {
            return await this.Add("sensor", new Dictionary<string, object>
            {
                { "name", sensor.Name },
                { "location", sensor.Location },
                { "type", sensor.Type }
            });
        }

        public async Task<IEnumerable<ISensorReading<double>>> QueryTemperature(Dictionary<string, object> queryParameters) => await this.Query<SensorReading<double>>("temperature", queryParameters);
        public async Task<IEnumerable<ISensorReading<double>>> QueryHumidity(Dictionary<string, object> queryParameters) => await this.Query<SensorReading<double>>("humidity", queryParameters);
        public async Task<IEnumerable<ISensor>> QuerySensors(Dictionary<string, object> queryParameters) => await this.Query<Sensor>("sensor", queryParameters);

        private async Task<bool> Add(string table, Dictionary<string, object> values)
        {

            var rows = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    var atKeys = new List<string>();

                    foreach (var val in values)
                    {
                        atKeys.Add($"@{val.Key}");
                        cmd.Parameters.AddWithValue($"@{val.Key}", val.Value);
                    }
                    
                    cmd.CommandText = $"INSERT INTO {table}({string.Join(", ", values.Keys)}) VALUES ({string.Join(", ", atKeys)})";
                    rows = await cmd.ExecuteNonQueryAsync();
                }
            }

            return rows != 0;
        }

        private async Task<IEnumerable<T>> Query<T>(string table, Dictionary<string, object> queryParameters) where T : ITimescaleRepresentable, new()
        {
            var readings = new List<T>();

            using (var conn = new NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
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

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
