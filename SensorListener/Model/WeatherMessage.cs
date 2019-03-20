using System;
using Domain.Entities;

namespace SensorListener.Model
{
    public class WeatherMessage
    {
        public int HomeId { get; set; }
        public Weather Weather { get; set; }
    }
}
