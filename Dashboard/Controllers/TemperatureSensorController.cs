using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManager.Common.Models;
using HomeManager.Common.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeManager.Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureSensorController : ControllerBase
    {
        private readonly IRepository<ISensorReading<double>> tempRepository;

        public TemperatureSensorController(IDBContext context) : base()
        {
            var sensor = new Sensor
            {
                Id = 1
            };
            this.tempRepository = new TempSensorReadingsRepository(context, sensor);
        }

        [HttpGet("[action]")]
        public IEnumerable<ISensorReading<double>> SensorReadings()
        {
            return this.tempRepository.GetAll();
        }
    }
}
