using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherSensor.Client
{
    interface IWeatherClient
    {
        Task<WeatherSensorReading> GetCurrentConditions();
    }
}
