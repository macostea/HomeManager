using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentsController : Controller
    {
        private readonly IHomeRepository homeRepository;

        public EnvironmentsController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var env = await this.homeRepository.GetEnvironment(id);
            if (env == null)
            {
                return NotFound();
            }
            return Ok(env);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Environment value)
        {
            var success = await this.homeRepository.EditEnvironment(value);
            if (!success)
            {
                return BadRequest();
            }
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var env = await this.homeRepository.GetEnvironment(id);
            if (env == null)
            {
                return NotFound();
            }
            var success = await this.homeRepository.DeleteEnvironment(id);
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(env);
        }
    }
}
