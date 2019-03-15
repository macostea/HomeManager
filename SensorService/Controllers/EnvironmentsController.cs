﻿using System.Threading.Tasks;
using Common.Repository;
using Domain.Entities;
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
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this.homeRepository.GetEnvironment(id));
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Environment value)
        {
            await this.homeRepository.EditEnvironment(value);
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var env = await this.homeRepository.GetEnvironment(id);
            await this.homeRepository.DeleteEnvironment(id);
            return Ok(env);
        }
    }
}
