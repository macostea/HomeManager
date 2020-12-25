using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Entities;

namespace BlazorDashboard.Services
{
    public class SensorServiceClient
    {
        public HttpClient httpClient { get; set; }

        public SensorServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Home>> GetHomes()
        {
            return await this.httpClient.GetFromJsonAsync<List<Home>>("api/homes");
        }

        public async Task<List<Room>> GetRooms(Home home)
        {
            return await this.httpClient.GetFromJsonAsync<List<Room>>("api/homes/" + home.Id.ToString() + "/room");
        }

        public async Task<List<Weather>> GetWeatherToday(Home home)
        {
            var startOfDay = DateTime.Today.ToUniversalTime();
            var now = DateTime.UtcNow;

            var uri = "api/homes/" + home.Id.ToString() + "/weather?startDate=" + startOfDay.ToString("s") + "&endDate=" + now.ToString("s");


            return await this.httpClient.GetFromJsonAsync<List<Weather>>(uri);
        }
    }
}
