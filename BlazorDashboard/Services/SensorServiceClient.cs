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
            return await this.httpClient.GetFromJsonAsync<List<Home>>("homes");
        }

        public async Task<List<Room>> GetRooms(Home home)
        {
            return await this.httpClient.GetFromJsonAsync<List<Room>>("homes/" + home.Id.ToString() + "/room");
        }

        //public async Task<List<Weather>> GetWeatherToday()
        //{

        //}
    }
}
