using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;
using Environment = Domain.Entities.Environment;

namespace Common.Repository
{
    public class HomeRepository: IHomeRepository
    {
        private readonly IDbConnection dbConnection;

        public HomeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<bool> AddEnvironmentReading(int roomId, int sensorId, Environment environment)
        {
            string sql = "INSERT INTO environment (timestamp, temperature, humidity, motion, sensor_id, room_id) VALUES (@Timestamp, @Temperature, @Humidity, @Motion, @SensorId, @RoomId)";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                environment.Timestamp,
                environment.Temperature,
                environment.Humidity,
                environment.Motion,
                SensorId = sensorId,
                RoomId = roomId
            });

            return affectedRows != 0;
        }

        public async Task<bool> AddHome(Home home)
        {
            string sql = "INSERT INTO homes (name, address, city, country) VALUES (@Name, @Address, @City, @Country)";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                home.Name,
                home.Address,
                home.City,
                home.Country
            });

            return affectedRows != 0;
        }

        public async Task<bool> AddRoom(int homeId, Room room)
        {
            var sql = "INSERT INTO rooms (name, home_id) VALUES (@Name, @HomeId)";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                room.Name,
                HomeId = homeId
            });

            return affectedRows != 0;
        }

        public async Task<bool> AddSensor(int roomId, Sensor sensor)
        {
            var sql = "INSERT INTO sensors (type, room_id) VALUES (@Type, @RoomId)";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                sensor.Type,
                room_id = roomId
            });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteEnvironment(int id)
        {
            var sql = "DELETE FROM environment WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteHome(int id)
        {
            var sql = "DELETE FROM homes WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var sql = "DELETE FROM rooms WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new { Id = id });

            return affectedRows != 0;
        }

        public async Task<bool> DeleteSensor(int id)
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
            	"motion = @Motion, " +
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
                "name = @Name, " +
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
                "WHERE id = @Id";

            var affectedRows = await dbConnection.ExecuteAsync(sql, new
            {
                sensor.Type,
                sensor.Id
            });

            return affectedRows != 0;
        }

        public async Task<Environment> GetEnvironment(int id)
        {
            var sql = "SELECT * FROM environment WHERE id = @Id";

            var env = await dbConnection.QueryFirstAsync<Environment>(sql, new { Id = id });
            return env;
        }

        public async Task<IEnumerable<Environment>> GetEnvironmentReadings(int roomId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM environment WHERE room_id = @RoomId AND timestamp >= @StartDate AND timestamp <= @EndDate";

            var envs = await dbConnection.QueryAsync<Environment>(sql, new
            {
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate
            });
            return envs.ToList();
        }

        public async Task<Home> GetHome(int id)
        {
            var sql = "SELECT * FROM homes WHERE id = @Id";

            var home = await dbConnection.QueryFirstAsync<Home>(sql, new { Id = id });
            return home;
        }

        public async Task<IEnumerable<Home>> GetHomes()
        {
            var sql = "SELECT * FROM homes";

            var homes = await dbConnection.QueryAsync<Home>(sql);
            return homes.ToList();
        }

        public async Task<Room> GetRoom(int id)
        {
            var sql = "SELECT * FROM rooms WHERE id = @Id";

            var room = await dbConnection.QueryFirstAsync<Room>(sql, new { Id = id });
            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms(Home home)
        {
            var sql = "SELECT * FROM rooms WHERE home_id = @HomeId";

            var rooms = await dbConnection.QueryAsync<Room>(sql, new { HomeId = home.Id });
            return rooms.ToList();
        }

        public async Task<Sensor> GetSensor(int id)
        {
            var sql = "SELECT * FROM sensors WHERE id = @Id";

            var sensor = await dbConnection.QueryFirstAsync<Sensor>(sql, new { Id = id });
            return sensor;
        }

        public async Task<IEnumerable<Sensor>> GetSensors(Room room)
        {
            var sql = "SELECT * FROM sensors WHERE room_id = @RoomId";

            var sensors = await dbConnection.QueryAsync<Sensor>(sql, new { RoomId = room.Id });
            return sensors.ToList();
        }
    }
}
