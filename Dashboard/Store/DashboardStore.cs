using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Environment = Domain.Entities.Environment;

namespace Dashboard.Store
{
    public class DashboardStore
    {
        public Home Home { get; set; }
        public Dictionary<Room, IList<Environment>> Rooms { get; set; }
        public Dictionary<Home, IList<Weather>> Weather { get; set; }
        public HttpClient Http { get; private set; }
        public Uri BaseUri { get; private set; }
        

        public DashboardStore(HttpClient http)
        {
            this.Home = new Home
            {
                Id = 1,
                Name = "dorobantilor",
                City = "Cluj-Napoca",
                Country = "Romania"
            };
            this.Http = http;
            this.BaseUri = new Uri("http://sensor-service.mcostea.com");
        }
        public async Task GetWeather()
        {
            var builder = new UriBuilder(this.BaseUri)
            {
                Path = string.Format($"api/homes/{0}/weather", this.Home.Id)
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            var today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            query["startDate"] = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0).ToString();
            query["endDate"] = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59).ToString();

            builder.Query = query.ToString();
            var weather = await this.Http.GetJsonAsync<IList<Weather>>(builder.ToString());

            this.Weather[Home] = weather;

            Console.WriteLine(weather);
        }
    }
}
