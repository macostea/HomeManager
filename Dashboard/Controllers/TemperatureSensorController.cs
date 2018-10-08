using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Common.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeManager.Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureSensorController : ControllerBase
    {

        public TemperatureSensorController() : base()
        {
            
        }
    }
}
