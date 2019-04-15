using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Environment = Domain.Entities.Environment;

namespace Dashboard.Store
{
    public class DashboardStore
    {
        public Home Home { get; set; }
        private Dictionary<Room, IList<Environment>> _rooms = new Dictionary<Room, IList<Environment>>();
        private Dictionary<Home, IList<Weather>> _weather = new Dictionary<Home, IList<Weather>>();
        private IList<Room> _roomList = new List<Room>();

        public Dictionary<Room, IList<Environment>> Rooms
        {
            get
            {
                lock (_rooms)
                {
                    return _rooms;
                }
            }
            set
            {
                lock (_rooms)
                {
                    _rooms = value;
                }
            }
        }
        public Dictionary<Home, IList<Weather>> Weather
        {
            get
            {
                lock (_weather)
                {
                    return _weather;
                }
            }
            set
            {
                lock (_weather)
                {
                    _weather = value;
                }
            }
        }

        public HttpClient Http { get; private set; }
        public Uri BaseUri { get; private set; }

        public IList<Action<DashboardStore>> Observers { get; private set; } = new List<Action<DashboardStore>>();

        private IList<Room> RoomList
        {
            get
            {
                lock (_roomList)
                {
                    return _roomList;
                }
            }
            set
            {
                lock (_roomList)
                {
                    _roomList = value;
                }
            }
        }
        

        public DashboardStore(HttpClient http)
        {
            Console.WriteLine("Init Dashboard Store starting");

            this.Home = new Home
            {
                Id = 1,
                Name = "dorobantilor",
                City = "Cluj-Napoca",
                Country = "Romania"
            };
            this.Http = http;
            this.BaseUri = new Uri("http://sensor-service.mcostea.com");

            Console.WriteLine("Init Dashboard Store Complete");
        }

        public void RegisterObserver(Action<DashboardStore> callback)
        {
            this.Observers.Add(callback);
        }

        public async Task GetWeather()
        {
            var builder = new UriBuilder(this.BaseUri)
            {
                Path = string.Format("api/homes/{0}/weather", this.Home.Id)
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            query["startDate"] = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0).ToString();
            query["endDate"] = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59).ToString();

            builder.Query = query.ToString();
            var weather = await this.Http.GetJsonAsync<IList<Weather>>(builder.ToString());

            lock (Weather)
            {
                this.Weather[Home] = weather;
            }

            this.NotifyObservers();
        }

        public async Task GetRooms()
        {
            var builder = new UriBuilder(this.BaseUri)
            {
                Path = string.Format("api/homes/{0}/room", this.Home.Id)
            };

            var rooms = await this.Http.GetJsonAsync<IList<Room>>(builder.ToString());
            this.RoomList = rooms;
        }

        public async Task GetEnvironment(Room room)
        {
            var builder = new UriBuilder(this.BaseUri)
            {
                Path = string.Format("api/rooms/{0}/environment", room.Id)
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            query["startDate"] = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0).ToString();
            query["endDate"] = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59).ToString();

            builder.Query = query.ToString();
            var environment = await this.Http.GetJsonAsync<IList<Environment>>(builder.ToString());

            lock (Rooms)
            {
                this.Rooms[room] = environment;
            }
        }

        public async Task RefreshAllRooms()
        {
            lock (Rooms)
            {
                this.Rooms.Clear();
            }

            foreach (var room in this.RoomList)
            {
                await this.GetEnvironment(room);
            }

            this.NotifyObservers();
        }

        protected void NotifyObservers()
        {
            foreach (var observer in this.Observers)
            {
                observer(this);
            }
        }
    }
}
