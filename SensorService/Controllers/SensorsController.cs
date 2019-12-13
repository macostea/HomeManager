using System;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Common.SensorListenerAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly IHomeRepository repository;
        private readonly ISensorListenerAPI listenerClient;

        public SensorsController(IHomeRepository repository, ISensorListenerAPI listenerClient)
        {
            this.repository = repository;
            this.listenerClient = listenerClient;
        }

        // POST api/sensor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Sensor sensor)
        {
            var insertedSensor = await this.repository.AddSensor(sensor);
            if (insertedSensor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (insertedSensor.RoomId != Guid.Empty)
            {
                var s = await listenerClient.NotifySensorUpdate(insertedSensor);
                return Ok(s);
            }

            return Ok(insertedSensor);
        }

        // PUT api/sensor
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Sensor sensor)
        {
            var resultOk = await this.repository.EditSensor(sensor);
            if (!resultOk)
            {
                return BadRequest();
            }

            var s = await this.listenerClient.NotifySensorUpdate(sensor);

            return Ok(s);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var sensor = await this.repository.GetSensor(Guid.Parse(id));
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sensors = await this.repository.GetSensors();
            return Ok(sensors);
        }

        // DELETE api/sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var sensor = await this.repository.GetSensor(Guid.Parse(id));
            if (sensor == null)
            {
                return NotFound();
            }

            var resultOk = await this.repository.DeleteSensor(Guid.Parse(id));
            if (!resultOk)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(sensor);
        }
    }
}
