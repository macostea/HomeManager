﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Models;
using HomeManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureSensorController : ControllerBase
    {
        private readonly IRepository<ISensorReading<double>> tempRepository;

        public TemperatureSensorController(IRepository<ISensorReading<double>> tempRepository) : base()
        {
            this.tempRepository = tempRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<ISensorReading<double>> SensorReadings()
        {
            var sensor = new TempSensor("office", this.tempRepository);
            return sensor.GetReadings();
        }
    }
}
