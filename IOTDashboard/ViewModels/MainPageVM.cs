using Common.SensorServiceAPI;
using Domain.Entities;
using IOTDashboard.Infrastructure.Base;
using IOTDashboard.Infrastructure.Services;
using IOTDashboard.Model;
using IOTDashboard.Utils;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Provider;

namespace IOTDashboard.ViewModels
{
    public class MainPageVM : ViewModelBase
    {
        public string OutsideTemperatureMessage { get; set; }
        public string OutsideConditionMessage { get; set; }
        public string WeatherTime { get; set; }
        public PlotModel EnvironmentModel { get; set; }
        private List<Room> _Rooms;
        public List<Room> Rooms
        {
            get
            {
                return _Rooms;
            }
            set
            {
                this._Rooms = value;
                this.NotifyPropertyChanged("Rooms");
            }
        }
        public DataService DataServiceImpl { get; }
        private Weather _LastWeather;
        public Weather LastWeather
        {
            get
            {
                return _LastWeather;
            }
            set
            {
                if (value != null)
                {
                    OutsideTemperatureMessage = $"Outside temperature: {value.Temperature}C";
                    OutsideConditionMessage = $"Outside condition: {value.Condition}";
                    WeatherTime = $"Last weather update: {value.Timestamp.ToLocalTime():F}";
                    this.NotifyPropertyChanged("OutsideTemperatureMessage");
                    this.NotifyPropertyChanged("OutsideConditionMessage");
                    this.NotifyPropertyChanged("WeatherTime");
                }
                _LastWeather = value;
            }
        }

        private Room _SelectedRoom;
        public Room SelectedRoom
        {
            get
            {
                return _SelectedRoom;
            }
            set
            {
                _SelectedRoom = value;
                _ = this.RefreshEnv();
            }
        }

        public string RoomTemperatureMessage { get; set; }
        public string RoomHumidityMessage { get; set; }

        private Domain.Entities.Environment _LastEnvironment;
        public Domain.Entities.Environment LastEnvironment
        {
            get
            {
                return _LastEnvironment;
            }
            set
            {
                if (value != null)
                {
                    RoomTemperatureMessage = $"{value.Temperature}C";
                    RoomHumidityMessage = $"{value.Humidity}%";
                }
                else
                {
                    RoomTemperatureMessage = "N/A";
                    RoomHumidityMessage = "N/A";
                }
                _LastEnvironment = value;

                this.NotifyPropertyChanged("RoomTemperatureMessage");
                this.NotifyPropertyChanged("RoomHumidityMessage");
            }
        }

        public MainPageVM(DataService dataService, ICommonServices commonServices) : base(commonServices)
        {
            DataServiceImpl = dataService;
        }

        public async Task LoadAsync()
        {
            var weatherList = await this.DataServiceImpl.GetWeather();
            this.LastWeather = weatherList.Last();

            this.Rooms = await this.DataServiceImpl.GetRooms();
            this.SelectedRoom = this.Rooms[0];
        }

        public async Task RefreshEnv()
        {
            var environment = await this.DataServiceImpl.GetEnvironment(SelectedRoom);

            if (environment.Count != 0)
            {
                this.EnvironmentModel = new PlotModel { Title = "Environment" };
                EnvironmentModel.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "HH:mm",
                    IntervalType = DateTimeIntervalType.Minutes,
                    IntervalLength = Axis.ToDouble(TimeSpan.FromMinutes(30))
                });

                EnvironmentModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    IntervalLength = 10,
                    Key = "temperature"
                });
                EnvironmentModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Right,
                    Minimum = 0,
                    Maximum = 100,
                    Key = "humidity"
                });
                var temperatureSeries = new LineSeries
                {
                    YAxisKey = "temperature"
                };
                var humiditySeries = new LineSeries
                {
                    YAxisKey = "humidity"
                };

                foreach (var env in environment)
                {
                    temperatureSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(env.Timestamp.ToLocalTime()), env.Temperature));
                    humiditySeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(env.Timestamp.ToLocalTime()), env.Humidity));
                }

                EnvironmentModel.Series.Add(temperatureSeries);
                EnvironmentModel.Series.Add(humiditySeries);

                this.NotifyPropertyChanged("EnvironmentModel");
            }


            this.LastEnvironment = environment.Count != 0 ? environment.Last() : null;
        }
    }
}
