using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class WeatherSensorReadingsController : Controller
    {
        private readonly IRepository<WeatherSensorReading> repository;
        private readonly IRepository<Sensor> sensorRepository;

        public WeatherSensorReadingsController(IRepository<WeatherSensorReading> repository,
                                                   IRepository<Sensor> sensorRepository)
        {
            this.repository = repository;
            this.sensorRepository = sensorRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<WeatherSensorReading>> Get()
        {
            return await this.repository.GetAll(new WeatherReadingWithSensorSpecification());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetWeatherReadings")]
        public async Task<IActionResult> Get(int id)
        {
            var results = await this.repository.GetAll(new WeatherReadingWithSensorSpecification(id));
            return results == null || !results.Any() ? NotFound() : (IActionResult)Ok(results.First());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WeatherSensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await this.sensorRepository.GetById(sensorReading.SensorId);
            if (sensor.Type != SensorType.Weather)
            {
                return BadRequest();
            }

            return await this.repository.Add(sensorReading)
                             ? CreatedAtRoute("GetWeatherReadings", new { Controller = "WeatherSensorReadings", id = sensorReading.SensorReadingId }, sensorReading)
                      : (IActionResult)BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]WeatherSensorReading reading)
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
