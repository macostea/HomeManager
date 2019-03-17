using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Environment = Domain.Entities.Environment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IHomeRepository homeRepository;

        public RoomsController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await this.homeRepository.GetRoom(id);
            return Ok(room);
        }

        // PUT api/values
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Room room)
        {
            await this.homeRepository.EditRoom(room);
            return Ok(room);
        }

        [HttpGet]
        public async Task<IActionResult> GetBySensor([FromQuery(Name = "sensorId")]int sensorId)
        {
            var room = await this.homeRepository.GetRoomBySensorId(sensorId);
            return Ok(room);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await this.homeRepository.GetRoom(id);
            await this.homeRepository.DeleteRoom(id);

            return Ok(room);
        }

        [HttpGet("{id}/sensor")]
        public async Task<IActionResult> GetSensors(int id)
        {
            var room = await this.homeRepository.GetRoom(id);
            var sensors = await this.homeRepository.GetSensors(room);
            return Ok(sensors);
        }

        [HttpPost("{id}/sensor")]
        public async Task<IActionResult> PostSensor(int id, [FromBody]Sensor sensor)
        {
            await this.homeRepository.AddSensor(id, sensor);
            return Ok(sensor);
        }

        [HttpGet("{id}/environment")]
        public async Task<IActionResult> GetEnvironment(int id, DateTime startDate, DateTime endDate)
        {
            var envs = await this.homeRepository.GetEnvironmentReadings(id, startDate, endDate);
            return Ok(envs);
        }

        [HttpPost("{id}/sensor/{sensorId}/environment")]
        public async Task<IActionResult> PostEnvironment(int id, int sensorId, [FromBody]Environment environment)
        {
            var insertedEnvironment = await this.homeRepository.AddEnvironmentReading(id, sensorId, environment);
            return Ok(insertedEnvironment);
        }
    }
}
