using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagerWeb.Models;
using HomeManagerWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeManager_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<ISensorReading<double>> tempRepository;

        public IndexModel(IRepository<ISensorReading<double>> tempRepository)
        {
            this.tempRepository = tempRepository;
        }

        public void OnGet()
        {
            var sensor = new TempSensor("livin-room", this.tempRepository);
            sensor.GetReadings().ForEach((obj) => Console.WriteLine(obj));
        }
    }
}
