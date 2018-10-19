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
    public class HumiditySensorReadingsController : Controller
    {
        private readonly IRepository<HumiditySensorReading> repository;
        private readonly IRepository<Sensor> sensorRepository;

        public HumiditySensorReadingsController(IRepository<HumiditySensorReading> repository,
                                                IRepository<Sensor> sensorRepository)
        {
            this.repository = repository;
            this.sensorRepository = sensorRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<HumiditySensorReading>> Get()
        {
            return await this.repository.GetAll(new HumidityReadingWithSensorSpecification());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var results = await this.repository.GetAll(new HumidityReadingWithSensorSpecification(id));
            return results == null || !results.Any() ? NotFound() : (IActionResult)Ok(results.First());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HumiditySensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await this.sensorRepository.GetById(sensorReading.SensorId);
            if (sensor.Type != SensorType.Humidity)
            {
                return BadRequest();
            }

            return await this.repository.Add(sensorReading)
                             ? CreatedAtRoute("GetReadings", new { Controller = "TemperatureSensorReadings", id = sensorReading.SensorReadingId }, sensorReading)
                      : (IActionResult)BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]HumiditySensorReading reading)
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
