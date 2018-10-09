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
    public class SensorsController : Controller
    {
        private readonly IRepository<Sensor> repository;

        public SensorsController(IRepository<Sensor> repository)
        {
            this.repository = repository;
        }

        // GET: api/sensor
        [HttpGet]
        public async Task<IEnumerable<Sensor>> Get()
        {
            return await this.repository.GetAll();
        }

        // GET api/sensor/5
        [HttpGet("{id}", Name = "GetSensors")]
        public async Task<Sensor> Get(int id)
        {
            return await this.repository.GetById(id);
        }

        // POST api/sensor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Sensor sensor)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : (await this.repository.Add(sensor)
                    ? CreatedAtRoute("GetSensors", new { Controller = "Sensors", id = sensor.SensorId }, sensor)
                    : (IActionResult)BadRequest());
        }

        // PUT api/sensor/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Sensor sensor)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : (await this.repository.Edit(sensor)
                    ? Ok(sensor)
                    : (IActionResult)BadRequest());
        }

        // DELETE api/sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await this.repository.GetById(id);

            return sensor != null 
                ? (await this.repository.Delete(sensor) 
                    ? Ok()
                    : (IActionResult)BadRequest())
                : NotFound();
        }
    }
}
