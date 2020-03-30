using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorListener.QueueClients;

namespace SensorListener.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly RabbitMQClient rabbitMQClient;

        public SensorsController(RabbitMQClient rabbitMQClient)
        {
            this.rabbitMQClient = rabbitMQClient;
        }

        [HttpPost]
        public async Task<IActionResult> NotifySensorUpdate([FromBody] Sensor sensor)
        {
            var sensorMessage = new Dictionary<string, string>()
            {
                { "RoomId", sensor.RoomId.ToString() }
            };
            await this.rabbitMQClient.PublishAsync(sensor.Id.ToString(), JsonConvert.SerializeObject(sensorMessage));
            return Ok(sensor);
        }
    }
}
