using Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using Environment = Domain.Entities.Environment;

namespace Dashboard.Store
{
    public class DashboardStore : IObservable<Weather>, IObservable<Dictionary<Room, Environment>>
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
        public IList<IObserver<Weather>> WeatherObservers { get; private set; } = new List<IObserver<Weather>>();
        public IList<IObserver<Dictionary<Room, Environment>>> EnvironmentObservers { get; private set; } = new List<IObserver<Dictionary<Room, Environment>>>();

        private Timer WeatherTimer;
        private Timer EnvironmentTimer;

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
                Id = "1",
                Name = "dorobantilor",
                City = "Cluj-Napoca",
                Country = "Romania"
            };
            this.Http = http;
            this.BaseUri = new Uri("http://sensor-service.mcostea.com");

            SetupWeatherTimer();
            SetupEnvironmentTimer();

            _ = GetWeather();
            _ = RefreshAllRooms();

            Console.WriteLine("Init Dashboard Store Complete");
        }

        private async Task GetWeather()
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
            var weather = (List<Weather>)await this.Http.GetJsonAsync<IList<Weather>>(builder.ToString());
            weather.Sort((w1, w2) => w1.Timestamp.CompareTo(w2.Timestamp));

            lock (Weather)
            {
                this.Weather[Home] = weather;
            }

            foreach (var observer in WeatherObservers)
            {
                observer.OnNext(weather.Last());
            }
        }

        private async Task GetRooms()
        {
            var builder = new UriBuilder(this.BaseUri)
            {
                Path = string.Format("api/homes/{0}/room", this.Home.Id)
            };

            var rooms = await this.Http.GetJsonAsync<IList<Room>>(builder.ToString());
            this.RoomList = rooms;
        }

        private async Task GetEnvironment(Room room)
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
            var environment = (List<Environment>)await this.Http.GetJsonAsync<IList<Environment>>(builder.ToString());
            environment.Sort((e1, e2) => e1.Timestamp.CompareTo(e2.Timestamp));

            lock (Rooms)
            {
                this.Rooms[room] = environment;
            }
        }

        private async Task RefreshAllRooms()
        {
            await this.GetRooms();
            lock (Rooms)
            {
                this.Rooms.Clear();
            }

            var latestEnvironment = new Dictionary<Room, Environment>();

            foreach (var room in this.RoomList)
            {
                await this.GetEnvironment(room).ConfigureAwait(false);
                latestEnvironment[room] = this.Rooms[room].Last();
            }

            foreach (var observer in EnvironmentObservers)
            {
                observer.OnNext(latestEnvironment);
            }
        }

        public IDisposable Subscribe(IObserver<Weather> observer)
        {
            if (!WeatherObservers.Contains(observer))
            {
                WeatherObservers.Add(observer);
                foreach (var home in Weather.Keys)
                {
                    foreach (var weather in Weather[home])
                    {
                        observer.OnNext(weather);
                    }
                }
            }

            return new Unsubscriber<Weather>(WeatherObservers, observer);
        }

        public IDisposable Subscribe(IObserver<Dictionary<Room, Environment>> observer)
        {
            if (!EnvironmentObservers.Contains(observer))
            {
                EnvironmentObservers.Add(observer);
                foreach (var room in Rooms.Keys)
                {
                    foreach (var environment in Rooms[room])
                    {
                        observer.OnNext(new Dictionary<Room, Environment>
                        {
                            [room] = environment
                        });
                    }
                }
            }

            return new Unsubscriber<Dictionary<Room, Environment>>(EnvironmentObservers, observer);
        }

        private void SetupWeatherTimer()
        {
            WeatherTimer = new Timer(1 * 1000 * 60 * 60);
            WeatherTimer.AutoReset = true;
            WeatherTimer.Elapsed += async (sender, args) =>
            {
                await this.GetWeather().ConfigureAwait(false);
            };

            WeatherTimer.Start();
        }

        private void SetupEnvironmentTimer()
        {
            EnvironmentTimer = new Timer(15 * 1000 * 60);
            EnvironmentTimer.AutoReset = true;
            EnvironmentTimer.Elapsed += async (sender, args) =>
            {
                await this.GetRooms().ConfigureAwait(false);
                await this.RefreshAllRooms().ConfigureAwait(false);
            };

            EnvironmentTimer.Start();
        }
    }
}
