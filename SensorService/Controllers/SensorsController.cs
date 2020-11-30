using System;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Common.SensorListenerAPI;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly IHomeRepository repository;
        private readonly ISensorListenerAPI listenerClient;
        private readonly ILogger<SensorsController> logger;

        public SensorsController(IHomeRepository repository, ISensorListenerAPI listenerClient, ILogger<SensorsController> logger)
        {
            this.repository = repository;
            this.listenerClient = listenerClient;
            this.logger = logger;
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
                if (s == null)
                {
                    this.logger.LogWarning("Failed to notify sensor change on queue client");
                }
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

        [HttpGet("{id}/homeymapping")]
        public async Task<IActionResult> GetHomeyMapping(string id)
        {
            var sensor = await this.repository.GetSensor(Guid.Parse(id));
            if (sensor == null)
            {
                return NotFound();
            }

            var homeyMapping = await this.repository.GetHomeyMapping(sensor);
            return Ok(homeyMapping);
        }

        [HttpPost("{id}/homeymapping")]
        public async Task<IActionResult> PostHomeyMapping(string id, [FromBody] HomeyMapping mapping)
        {
            var sensor = await this.repository.GetSensor(Guid.Parse(id));
            if (sensor == null)
            {
                return NotFound();
            }

            var insertedMapping = await this.repository.AddHomeyMapping(Guid.Parse(id), mapping);
            if (insertedMapping == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(insertedMapping);
        }
    }
}
