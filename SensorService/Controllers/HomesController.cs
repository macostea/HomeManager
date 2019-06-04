using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorService.Controllers
{
    [Route("api/[controller]")]
    public class HomesController : Controller
    {
        private readonly IHomeRepository homeRepository;

        public HomesController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.homeRepository.GetHomes());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }

            return Ok(await this.homeRepository.GetHome(id));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Home value)
        {
            var insertedHome = await this.homeRepository.AddHome(value);
            if (insertedHome == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(insertedHome);
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Home value)
        {
            var success = await this.homeRepository.EditHome(value);
            if (!success)
            {
                return BadRequest();
            }
            return Ok(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }

            var success = await this.homeRepository.DeleteHome(id);

            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(home);
        }

        [HttpGet("{id}/room")]
        public async Task<IActionResult> GetRooms(string id)
        {
            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }

            var rooms = await this.homeRepository.GetRooms(home);
            return Ok(rooms);
        }

        [HttpPost("{id}/room")]
        public async Task<IActionResult> AddRoom(string id, [FromBody]Room room)
        {
            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }
            var insertedRoom = await this.homeRepository.AddRoom(id, room);
            if (insertedRoom == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(insertedRoom);
        }

        [HttpPost("{id}/weather")]
        public async Task<IActionResult> AddWeather(string id, [FromBody]Weather weather)
        {
            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }
            var insertedWeather = await this.homeRepository.AddWeather(id, weather);
            if (insertedWeather == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(insertedWeather);
        }

        [HttpGet("{id}/weather")]
        public async Task<IActionResult> GetWeather(string id, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var invalidDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (!ModelState.IsValid || startDate.Equals(endDate) || startDate.Equals(invalidDate) || endDate.Equals(invalidDate))
            {
                return BadRequest();
            }

            var home = await this.homeRepository.GetHome(id);
            if (home == null)
            {
                return NotFound();
            }

            var weather = await this.homeRepository.GetWeather(id, startDate, endDate);
            return Ok(weather);
        }
    }
}
