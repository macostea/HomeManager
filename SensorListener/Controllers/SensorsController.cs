using System;
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
        private RabbitMQClient rabbitMQClient;

        public SensorsController(RabbitMQClient rabbitMQClient)
        {
            this.rabbitMQClient = rabbitMQClient;
        }

        [HttpPost]
        public async Task<IActionResult> NotifySensorUpdate([FromBody] Sensor sensor)
        {
            await this.rabbitMQClient.PublishAsync(sensor.Id.ToString(), JsonConvert.SerializeObject(sensor));
            return Ok(sensor);
        }
    }
}
