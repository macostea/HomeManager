using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Environment = Domain.Entities.Environment;

namespace Common.Repository
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Home>> GetHomes();
        Task<Home> AddHome(Home home);
        Task<bool> EditHome(Home home);

        Task<Home> GetHome(Guid id);
        Task<bool> DeleteHome(Guid id);

        Task<IEnumerable<Room>> GetRooms(Home home);
        Task<Room> AddRoom(Guid homeId, Room room);

        Task<IEnumerable<Weather>> GetWeather(Guid homeId, DateTime startDate, DateTime endDate);
        Task<Weather> AddWeather(Guid homeId, Weather weather);

        Task<bool> EditRoom(Room room);

        Task<Room> GetRoom(Guid id);
        Task<bool> DeleteRoom(Guid id);

        Task<Room> GetRoomBySensorId(Guid sensorId);

        Task<IEnumerable<Sensor>> GetSensors(Room room);
        Task<Sensor> AddSensor(Guid roomId, Sensor sensor);

        Task<bool> EditWeather(Weather weather);

        Task<Weather> GetWeather(Guid id);
        Task<bool> DeleteWeather(Guid id);

        Task<IEnumerable<Environment>> GetEnvironmentReadings(Guid roomId, DateTime startDate, DateTime endDate);
        Task<Environment> AddEnvironmentReading(Guid roomId, Guid sensorId, Environment environment);

        Task<bool> EditSensor(Sensor sensor);

        Task<Sensor> GetSensor(Guid id);
        Task<bool> DeleteSensor(Guid id);

        Task<bool> EditEnvironment(Environment environment);
        Task<Environment> GetEnvironment(Guid id);
        Task<bool> DeleteEnvironment(Guid id);
    }
}
