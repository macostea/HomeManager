using Domain.Entities;
using System.Threading.Tasks;

namespace WeatherSensor.Client
{
    interface IWeatherClient
    {
        Task<Weather> GetCurrentConditions();
    }
}
