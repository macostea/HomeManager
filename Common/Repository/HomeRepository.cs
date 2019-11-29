using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Mappings;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Environment = Domain.Entities.Environment;

namespace Common.Repository
{
    public class HomeRepository: IHomeRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger<HomeRepository> logger;

        public HomeRepository(IDbConnection dbConnection, ILogger<HomeRepository> logger)
        {
            this.dbConnection = dbConnection;
            this.logger = logger;
        }

        public async Task<Environment> AddEnvironmentReading(Guid roomId, Guid sensorId, Environment environment)
        {
            string sql = "INSERT INTO environment (timestamp, temperature, humidity, motion, sensor_id, room_id) VALUES (@Timestamp, @Temperature, @Humidity, @Motion, @SensorId, @RoomId) RETURNING *";

            Environment insertedEnvironment = null;
            try
            {
                insertedEnvironment = await dbConnection.QueryFirstAsync<Environment>(sql, new
                {
                    environment.Timestamp,
                    environment.Temperature,
                    environment.Humidity,
                    environment.Motion,
                    SensorId = sensorId,
                    RoomId = roomId
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert environment");
            }

            return insertedEnvironment;
        }

        public async Task<Home> AddHome(Home home)
        {
            string sql = "INSERT INTO homes (name, address, city, country) VALUES (@Name, @Address, @City, @Country) RETURNING *";

            Home insertedHome = null;
            try
            {
                insertedHome = await dbConnection.QueryFirstAsync<Home>(sql, new
                {
                    home.Name,
                    home.Address,
                    home.City,
                    home.Country
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert home");
            }

            return insertedHome;
        }

        public async Task<Room> AddRoom(Guid homeId, Room room)
        {
            var sql = "INSERT INTO rooms (name, home_id) VALUES (@Name, @HomeId) RETURNING *";

            Room insertedRoom = null;
            try
            {
                insertedRoom = await dbConnection.QueryFirstAsync<Room>(sql, new
                {
                    room.Name,
                    HomeId = homeId
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert room");
            }

            return insertedRoom;
        }

        public async Task<Sensor> AddSensor(Guid roomId, Sensor sensor)
        {
            var sql = "INSERT INTO sensors (type, room_id) VALUES (@Type, @RoomId) RETURNING *";

            Sensor insertedSensor = null;
            try
            {
                insertedSensor = await dbConnection.QueryFirstAsync<Sensor>(sql, new
                {
                    sensor.Type,
                    RoomId = roomId
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert sensor");
            }

            return insertedSensor;
        }

        public async Task<Sensor> AddSensor(Sensor sensor)
        {
            var sql = "INSERT INTO sensors (type) VALUES (@Type) RETURNING *";
            object param = new
            {
                sensor.Type
            };

            if (sensor.Id != Guid.Empty)
            {
                sql = "INSERT INTO sensors (id, type) VALUES (@Id, @Type) ON CONFLICT (id) DO UPDATE SET type = @Type RETURNING *";
                param = new
                {
                    sensor.Id,
                    sensor.Type
                };
            }

            Sensor insertedSensor = null;
            try
            {
                insertedSensor = await dbConnection.QueryFirstAsync<Sensor>(sql, param);
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert sensor");
            }

            return insertedSensor;
        }

        public async Task<bool> DeleteEnvironment(Guid id)
        {
            var sql = "DELETE FROM environment WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteHome(Guid id)
        {
            var sql = "DELETE FROM homes WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteRoom(Guid id)
        {
            var sql = "DELETE FROM rooms WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteSensor(Guid id)
        {
            var sql = "DELETE FROM sensors WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> EditEnvironment(Environment environment)
        {
            var sql = "UPDATE environment SET " +
            	"timestamp = @Timestamp, " +
            	"temperature = @Temperature, " +
            	"humidity = @Humidity, " +
            	"motion = @Motion " +
            	"WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                environment.Timestamp,
                environment.Temperature,
                environment.Humidity,
                environment.Motion,
                environment.Id
            });

            return affectedRows != 0;
        }

        public async Task<bool> EditHome(Home home)
        {
            var sql = "UPDATE homes SET " +
                "name = @Name, " +
                "address = @Address, " +
                "city = @City, " +
                "country = @Country " +
                "WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                home.Name,
                home.Address,
                home.City,
                home.Country,
                home.Id
            });

            return affectedRows != 0;
        }

        public async Task<bool> EditRoom(Room room)
        {
            var sql = "UPDATE rooms SET " +
                "name = @Name " +
                "WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                room.Name,
                room.Id
            });

            return affectedRows != 0;
        }

        public async Task<bool> EditSensor(Sensor sensor)
        {
            var sql = "UPDATE sensors SET " +
                "type = @Type, " +
                "room_id = @RoomId " +
                "WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                sensor.Type,
                sensor.Id,
                sensor.RoomId
            });

            return affectedRows != 0;
        }

        public async Task<Environment> GetEnvironment(Guid id)
        {
            var sql = "SELECT * FROM environment WHERE id = @Id";

            Environment env = null;
            try
            {
                env = await dbConnection.QueryFirstAsync<Environment>(sql, new { Id = id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get environment");
            }

            return env;
        }

        public async Task<IEnumerable<Environment>> GetEnvironmentReadings(Guid roomId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM environment WHERE room_id = @RoomId AND timestamp >= @StartDate AND timestamp <= @EndDate";

            IEnumerable<Environment> envs = null;
            try
            {
                envs = await dbConnection.QueryAsync<Environment>(sql, new
                {
                    RoomId = roomId,
                    StartDate = startDate,
                    EndDate = endDate
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get environments");
            }

            return envs;
        }

        public async Task<Home> GetHome(Guid id)
        {
            var sql = "SELECT * FROM homes WHERE id = @Id";

            Home home = null;
            try
            {
                home = await dbConnection.QueryFirstAsync<Home>(sql, new { Id = id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get home");
            }

            return home;
        }

        public async Task<IEnumerable<Home>> GetHomes()
        {
            var sql = "SELECT * FROM homes";

            IEnumerable<Home> homes = null;
            try
            {
                homes = await dbConnection.QueryAsync<Home>(sql);
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get homes");
            }

            return homes;
        }

        public async Task<Room> GetRoom(Guid id)
        {
            var sql = "SELECT * FROM rooms WHERE id = @Id";

            Room room = null;
            try
            {
                room = await dbConnection.QueryFirstAsync<Room>(sql, new { Id = id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get room");
            }

            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms(Home home)
        {
            var sql = "SELECT * FROM rooms WHERE home_id = @HomeId";

            IEnumerable<Room> rooms = null;
            try
            {
                rooms = await dbConnection.QueryAsync<Room>(sql, new { HomeId = home.Id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get rooms");
            }
            
            return rooms;
        }

        public async Task<Room> GetRoomBySensorId(Guid sensorId)
        {
            var sql = "SELECT rooms.id, name, home_id FROM rooms " +
                "JOIN sensors " +
                "ON rooms.id = sensors.room_id " +
                "WHERE sensors.id = @Id";

            Room room = null;
            try
            {
                room = await dbConnection.QueryFirstAsync<Room>(sql, new { Id = sensorId });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get room");
            }

            return room;
        }

        public async Task<Sensor> GetSensor(Guid id)
        {
            var sql = "SELECT * FROM sensors WHERE id = @Id";

            Sensor sensor = null;
            try
            {
                sensor = await dbConnection.QueryFirstAsync<Sensor>(sql, new { Id = id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get sensor");
            }

            return sensor;
        }

        public async Task<IEnumerable<Sensor>> GetSensors(Room room)
        {
            var sql = "SELECT * FROM sensors WHERE room_id = @RoomId";

            IEnumerable<Sensor> sensors = null;
            try
            {
                sensors = await dbConnection.QueryAsync<Sensor>(sql, new { RoomId = room.Id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get sensors");
            }

            return sensors;
        }

        public async Task<IEnumerable<Sensor>> GetSensors()
        {
            var sql = "SELECT * FROM sensors";

            IEnumerable<Sensor> sensors = null;
            try
            {
                sensors = await dbConnection.QueryAsync<Sensor>(sql);
            }
            catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get sensors");
            }

            return sensors;
        }

        public async Task<IEnumerable<Weather>> GetWeather(Guid homeId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM weather WHERE home_id = @HomeId AND timestamp >= @StartDate AND timestamp <= @EndDate";

            IEnumerable<Weather> weather = null;
            try
            {
                weather = await dbConnection.QueryAsync<Weather>(sql, new
                {
                    HomeId = homeId,
                    StartDate = startDate,
                    EndDate = endDate
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get weather");
            }

            return weather;
        }

        public async Task<Weather> AddWeather(Guid homeId, Weather weather)
        {
            var sql = "INSERT INTO weather (timestamp, temperature, pressure, humidity, minimum_temperature, maximum_temperature, condition_code, condition, icon_url, home_id) " +
                "VALUES (@Timestamp, @Temperature, @Pressure, @Humidity, @MinimumTemperature, @MaximumTemperature, @ConditionCode, @Condition, @IconURL, @HomeId) " +
                "RETURNING *";

            Weather insertedWeather = null;
            try
            {
                insertedWeather = await dbConnection.QueryFirstAsync<Weather>(sql, new
                {
                    weather.Timestamp,
                    weather.Temperature,
                    weather.Pressure,
                    weather.Humidity,
                    weather.MinimumTemperature,
                    weather.MaximumTemperature,
                    weather.ConditionCode,
                    weather.Condition,
                    weather.IconURL,
                    HomeId = homeId
                });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot insert weather");
            }

            return weather;
        }

        public async Task<bool> EditWeather(Weather weather)
        {
            var sql = "UPDATE weather SET " +
                "timestamp = @Timestamp, " +
                "temperature = @Temperature, " +
                "pressure = @Pressure, " +
                "humidity = @Humidity, " +
                "minimumTemperature = @MinimumTemperature," +
                "maximumTemperature = @MaximumTemperature, " +
                "conditionCode = @ConditionCode, " +
                "conditions = @Condition, " +
                "iconURL = @IconURL " +
                "WHERE id = @Id";
            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                weather.Timestamp,
                weather.Temperature,
                weather.Pressure,
                weather.Humidity,
                weather.MinimumTemperature,
                weather.MaximumTemperature,
                weather.ConditionCode,
                weather.Condition,
                weather.IconURL,
                weather.Id
            });

            return affectedRows != 0;
        }

        public async Task<Weather> GetWeather(Guid id)
        {
            var sql = "SELECT * FROM weather WHERE id = @Id";

            Weather weather = null;
            try
            {
                weather = await dbConnection.QueryFirstAsync<Weather>(sql, new { Id = id });
            } catch (InvalidOperationException e)
            {
                this.logger.LogError(e, "Cannot get weather");
            }

            return weather;
        }

        public async Task<bool> DeleteWeather(Guid id)
        {
            var sql = "DELETE FROM weather WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }
    }
}
