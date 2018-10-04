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

        public TemperatureSensorReadingsController(IRepository<TemperatureSensorReading> repository)
        {
            this.repository = repository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<TemperatureSensorReading>> Get()
        {
            return await this.repository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetReadings")]
        public async Task<TemperatureSensorReading> Get(int id)
        {
            return await this.repository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TemperatureSensorReading sensorReading)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : (await this.repository.Add(sensorReading)
                    ? CreatedAtRoute("GetReadings", new { Controller = "TemperatureSensorReadings", id = sensorReading.Id }, sensorReading)
                    : (IActionResult)BadRequest());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
