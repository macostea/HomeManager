using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Common.SensorListenerAPI;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Environment = Domain.Entities.Environment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IHomeRepository homeRepository;
        private readonly ISensorListenerAPI listenerClient;

        public RoomsController(IHomeRepository homeRepository, ISensorListenerAPI listenerClient)
        {
            this.homeRepository = homeRepository;
            this.listenerClient = listenerClient;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var room = await this.homeRepository.GetRoom(Guid.Parse(id));

            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // PUT api/values
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Room room)
        {
            var success = await this.homeRepository.EditRoom(room);
            if (!success)
            {
                return BadRequest();
            }
            return Ok(room);
        }

        [HttpGet]
        public async Task<IActionResult> GetBySensor([FromQuery(Name = "sensorId")]string sensorId)
        {
            var room = await this.homeRepository.GetRoomBySensorId(Guid.Parse(sensorId));
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var room = await this.homeRepository.GetRoom(Guid.Parse(id));
            if (room == null)
            {
                return NotFound();
            }
            var success = await this.homeRepository.DeleteRoom(Guid.Parse(id));
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(room);
        }

        [HttpGet("{id}/sensor")]
        public async Task<IActionResult> GetSensors(string id)
        {
            var room = await this.homeRepository.GetRoom(Guid.Parse(id));
            if (room == null)
            {
                return NotFound();
            }
            var sensors = await this.homeRepository.GetSensors(room);
            return Ok(sensors);
        }

        [HttpPost("{id}/sensor")]
        public async Task<IActionResult> PostSensor(string id, [FromBody]Sensor sensor)
        {
            var room = await this.homeRepository.GetRoom(Guid.Parse(id));
            if (room == null)
            {
                return NotFound();
            }
            var insertedSensor = await this.homeRepository.AddSensor(Guid.Parse(id), sensor);
            if (insertedSensor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(sensor);
        }

        [HttpGet("{id}/environment")]
        public async Task<IActionResult> GetEnvironment(string id, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var invalidDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (!ModelState.IsValid || startDate.Equals(endDate) || startDate.Equals(invalidDate) || endDate.Equals(invalidDate))
            {
                return BadRequest();
            }

            var room = await this.homeRepository.GetRoom(Guid.Parse(id));
            if (room == null)
            {
                return NotFound();
            }

            var envs = await this.homeRepository.GetEnvironmentReadings(Guid.Parse(id), startDate, endDate);
            return Ok(envs);
        }

        [HttpPost("{id}/sensor/{sensorId}/environment")]
        public async Task<IActionResult> PostEnvironment(string id, string sensorId, [FromBody]Environment environment)
        {
            var room = await this.homeRepository.GetRoom(Guid.Parse(id));
            if (room == null)
            {
                return NotFound();
            }

            var sensor = await this.homeRepository.GetSensor(Guid.Parse(sensorId));
            if (sensor == null)
            {
                return NotFound();
            }

            var insertedEnvironment = await this.homeRepository.AddEnvironmentReading(Guid.Parse(id), Guid.Parse(sensorId), environment);
            if (insertedEnvironment == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var homeyMapping = await this.homeRepository.GetHomeyMapping(sensor);
            if (homeyMapping != null)
            {
                if (homeyMapping.HumTopic != null && !insertedEnvironment.Humidity.Equals(0.0))
                {
                    await this.listenerClient.NotifyHomeyTopic<double>(homeyMapping.HumTopic, insertedEnvironment.Humidity);
                }
                if (homeyMapping.TempTopic != null && !insertedEnvironment.Temperature.Equals(0.0))
                {
                    await this.listenerClient.NotifyHomeyTopic<double>(homeyMapping.TempTopic, insertedEnvironment.Temperature);
                }
                if (homeyMapping.MotionTopic != null)
                {
                    await this.listenerClient.NotifyHomeyTopic<bool>(homeyMapping.MotionTopic, insertedEnvironment.Motion);
                }
            }

            return Ok(insertedEnvironment);
        }
    }
}
