﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
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
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this.homeRepository.GetHome(id));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Home value)
        {
            var insertedHome = await this.homeRepository.AddHome(value);
            return Ok(insertedHome);
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Home value)
        {
            await this.homeRepository.EditHome(value);
            return Ok(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var home = await this.homeRepository.GetHome(id);
            await this.homeRepository.DeleteHome(id);

            return Ok(home);
        }

        [HttpGet("{id}/room")]
        public async Task<IActionResult> GetRooms(int id)
        {
            var home = await this.homeRepository.GetHome(id);
            var rooms = await this.homeRepository.GetRooms(home);
            return Ok(rooms);
        }

        [HttpPost("{id}/room")]
        public async Task<IActionResult> AddRoom(int id, [FromBody]Room room)
        {
            var insertedRoom = await this.homeRepository.AddRoom(id, room);
            return Ok(insertedRoom);
        }

        [HttpPost("{id}/weather")]
        public async Task<IActionResult> AddWeather(int id, [FromBody]Weather weather)
        {
            var insertedWeather = await this.homeRepository.AddWeather(id, weather);
            return Ok(insertedWeather);
        }

        [HttpGet("{id}/weather")]
        public async Task<IActionResult> GetWeather(int id, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var weather = await this.homeRepository.GetWeather(id, startDate, endDate);
            return Ok(weather);
        }
    }
}