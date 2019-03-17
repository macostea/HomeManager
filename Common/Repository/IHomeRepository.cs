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

        Task<Home> GetHome(int id);
        Task<bool> DeleteHome(int id);

        Task<IEnumerable<Room>> GetRooms(Home home);
        Task<Room> AddRoom(int homeId, Room room);

        Task<bool> EditRoom(Room room);

        Task<Room> GetRoom(int id);
        Task<bool> DeleteRoom(int id);

        Task<Room> GetRoomBySensorId(int sensorId);

        Task<IEnumerable<Sensor>> GetSensors(Room room);
        Task<Sensor> AddSensor(int roomId, Sensor sensor);

        Task<IEnumerable<Environment>> GetEnvironmentReadings(int roomId, DateTime startDate, DateTime endDate);
        Task<Environment> AddEnvironmentReading(int roomId, int sensorId, Environment environment);

        Task<bool> EditSensor(Sensor sensor);

        Task<Sensor> GetSensor(int id);
        Task<bool> DeleteSensor(int id);

        Task<bool> EditEnvironment(Environment environment);
        Task<Environment> GetEnvironment(int id);
        Task<bool> DeleteEnvironment(int id);
    }
}
