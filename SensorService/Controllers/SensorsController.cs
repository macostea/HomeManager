using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class SensorsController : Controller
    {
        private readonly IHomeRepository repository;

        public SensorsController(IHomeRepository repository)
        {
            this.repository = repository;
        }

        // PUT api/sensor
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Sensor sensor)
        {
            await this.repository.EditSensor(sensor);
            return Ok(sensor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sensor = await this.repository.GetSensor(id);
            return Ok(sensor);
        }

        // DELETE api/sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await this.repository.GetSensor(id);
            await this.repository.DeleteSensor(id);
            return Ok(sensor);
        }
    }
}
