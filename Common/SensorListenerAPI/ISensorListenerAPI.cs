using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Common.SensorListenerAPI
{
    public interface ISensorListenerAPI
    {
        public Task<Sensor> NotifySensorUpdate(Sensor sensor);
    }
}
