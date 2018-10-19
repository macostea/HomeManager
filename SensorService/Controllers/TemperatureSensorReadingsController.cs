using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureSensorReadingsController : Controller
    {
        private readonly IRepository<TemperatureSensorReading> repository;
        private readonly IRepository<Sensor> sensorRepository;

        public TemperatureSensorReadingsController(IRepository<TemperatureSensorReading> repository,
                                                   IRepository<Sensor> sensorRepository)
        {
            this.repository = repository;
            this.sensorRepository = sensorRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<TemperatureSensorReading>> Get()
        {
            return await this.repository.GetAll(new TempReadingWithSensorSpecification());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetReadings")]
        public async Task<IActionResult> Get(int id)
        {
            var results = await this.repository.GetAll(new TempReadingWithSensorSpecification(id));
            return results == null || !results.Any() ? NotFound() : (IActionResult)Ok(results.First());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TemperatureSensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await this.sensorRepository.GetById(sensorReading.SensorId);
            if (sensor.Type != SensorType.Temperature)
            {
                return BadRequest();
            }

            return await this.repository.Add(sensorReading)
                             ? CreatedAtRoute("GetReadings", new { Controller = "TemperatureSensorReadings", id = sensorReading.SensorReadingId }, sensorReading)
                      : (IActionResult)BadRequest();

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]TemperatureSensorReading reading)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : (await this.repository.Edit(reading)
                    ? Ok()
                    : (IActionResult)NotFound());
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await this.repository.GetById(id) != null
                ? (await this.repository.Delete(await this.repository.GetById(id))
                    ? Ok()
                    : (IActionResult)NotFound())
                : NotFound();
        }
    }
}
