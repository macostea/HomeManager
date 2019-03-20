﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
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

        public RoomsController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await this.homeRepository.GetRoom(id);

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
        public async Task<IActionResult> GetBySensor([FromQuery(Name = "sensorId")]int sensorId)
        {
            var room = await this.homeRepository.GetRoomBySensorId(sensorId);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await this.homeRepository.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            var success = await this.homeRepository.DeleteRoom(id);
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(room);
        }

        [HttpGet("{id}/sensor")]
        public async Task<IActionResult> GetSensors(int id)
        {
            var room = await this.homeRepository.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            var sensors = await this.homeRepository.GetSensors(room);
            return Ok(sensors);
        }

        [HttpPost("{id}/sensor")]
        public async Task<IActionResult> PostSensor(int id, [FromBody]Sensor sensor)
        {
            var room = await this.homeRepository.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }
            var insertedSensor = await this.homeRepository.AddSensor(id, sensor);
            if (insertedSensor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(sensor);
        }

        [HttpGet("{id}/environment")]
        public async Task<IActionResult> GetEnvironment(int id, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var invalidDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (!ModelState.IsValid || startDate.Equals(endDate) || startDate.Equals(invalidDate) || endDate.Equals(invalidDate))
            {
                return BadRequest();
            }

            var room = await this.homeRepository.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }

            var envs = await this.homeRepository.GetEnvironmentReadings(id, startDate, endDate);
            return Ok(envs);
        }

        [HttpPost("{id}/sensor/{sensorId}/environment")]
        public async Task<IActionResult> PostEnvironment(int id, int sensorId, [FromBody]Environment environment)
        {
            var room = await this.homeRepository.GetRoom(id);
            if (room == null)
            {
                return NotFound();
            }

            var sensor = await this.homeRepository.GetSensor(sensorId);
            if (sensor == null)
            {
                return NotFound();
            }

            var insertedEnvironment = await this.homeRepository.AddEnvironmentReading(id, sensorId, environment);
            if (insertedEnvironment == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(insertedEnvironment);
        }
    }
}
