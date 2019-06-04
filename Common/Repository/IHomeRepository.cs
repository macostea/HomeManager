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

        Task<Home> GetHome(string id);
        Task<bool> DeleteHome(string id);

        Task<IEnumerable<Room>> GetRooms(Home home);
        Task<Room> AddRoom(string homeId, Room room);

        Task<IEnumerable<Weather>> GetWeather(string homeId, DateTime startDate, DateTime endDate);
        Task<Weather> AddWeather(string homeId, Weather weather);

        Task<bool> EditRoom(Room room);

        Task<Room> GetRoom(string id);
        Task<bool> DeleteRoom(string id);

        Task<Room> GetRoomBySensorId(string sensorId);

        Task<IEnumerable<Sensor>> GetSensors(Room room);
        Task<Sensor> AddSensor(string roomId, Sensor sensor);

        Task<bool> EditWeather(Weather weather);

        Task<Weather> GetWeather(string id);
        Task<bool> DeleteWeather(string id);

        Task<IEnumerable<Environment>> GetEnvironmentReadings(string roomId, DateTime startDate, DateTime endDate);
        Task<Environment> AddEnvironmentReading(string roomId, string sensorId, Environment environment);

        Task<bool> EditSensor(Sensor sensor);

        Task<Sensor> GetSensor(string id);
        Task<bool> DeleteSensor(string id);

        Task<bool> EditEnvironment(Environment environment);
        Task<Environment> GetEnvironment(string id);
        Task<bool> DeleteEnvironment(string id);
    }
}
