using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
            var resultOk = await this.repository.EditSensor(sensor);
            if (!resultOk)
            {
                return BadRequest();
            }

            return Ok(sensor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var sensor = await this.repository.GetSensor(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }

        // DELETE api/sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var sensor = await this.repository.GetSensor(id);
            if (sensor == null)
            {
                return NotFound();
            }

            var resultOk = await this.repository.DeleteSensor(id);
            if (!resultOk)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(sensor);
        }
    }
}
