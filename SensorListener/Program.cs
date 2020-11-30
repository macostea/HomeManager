using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SensorListener;
using SensorListener.Listeners;
using SensorListener.QueueClients;
using System;

namespace HomeManager.SensorListener
{
    static class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
