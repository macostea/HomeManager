using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Common.SensorListenerAPI
{
    public interface ISensorListenerAPI
    {
        Task<Sensor> NotifySensorUpdate(Sensor sensor);
        Task NotifyHomeyTopic<T>(string topic, T message); 
    }
}
