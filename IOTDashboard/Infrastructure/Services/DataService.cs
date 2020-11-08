using Common.SensorServiceAPI;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Provider;

namespace IOTDashboard.Model
{
    public class DataService
    {
        public SensorServiceAPI Client { get; }
        public string HomeId { get; }

        public DataService()
        {
            this.HomeId = "24039b7c-bf70-4a7d-b562-96ff4382ba78";
            this.Client = new SensorServiceAPI("http://sensor-service.mcostea.com");
        }

        public List<Weather> LastWeather;
        public DateTime WeatherFetchDateTime;
        public async Task<List<Weather>> GetWeather()
        {
            var shouldFetch = true;
            if (LastWeather != null && WeatherFetchDateTime != null)
            {
                if (DateTime.Now - WeatherFetchDateTime < TimeSpan.FromMinutes(30)) // Only fetch every 30 mins, otherwise use cached data.
                {
                    shouldFetch = false;
                }
            }

            if (shouldFetch)
            {
                var weatherList = await Client.GetWeather(HomeId, DateTime.Today, DateTime.Now);

                weatherList.Sort((w1, w2) => w1.Timestamp.CompareTo(w2.Timestamp));

                if (weatherList.Count == 0)
                {
                    throw new Exception("Failed to retrieve weather");
                }

                this.LastWeather = weatherList;
                this.WeatherFetchDateTime = DateTime.Now;
            }

            return LastWeather;
        }

        public List<Room> Rooms;
        public DateTime RoomsFetchDateTime;
        public async Task<List<Room>> GetRooms()
        {
            var shouldFetch = true;
            if (Rooms != null && RoomsFetchDateTime != null)
            {
                if (DateTime.Now - RoomsFetchDateTime < TimeSpan.FromHours(1))
                {
                    shouldFetch = false;
                }
            }

            if (shouldFetch)
            {
                var roomsList = await Client.GetRooms(HomeId);
                roomsList.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));

                if (roomsList.Count == 0)
                {
                    throw new Exception("Failed to retrieve rooms");
                }

                this.Rooms = roomsList;
                this.RoomsFetchDateTime = DateTime.Now;
            }

            return this.Rooms;
        }

        public Dictionary<Room, List<Domain.Entities.Environment>> Environment = new Dictionary<Room, List<Domain.Entities.Environment>>();
        
        public async Task<List<Domain.Entities.Environment>> GetEnvironment(Room room)
        {
            var shouldFetch = true;
            var environment = await Client.GetEnvironment(room.Id.ToString(), DateTime.Today, DateTime.Now);

            environment.Sort((e1, e2) => e1.Timestamp.CompareTo(e2.Timestamp));

            this.Environment[room] = environment;

            return this.Environment[room];
        }
    }
}
