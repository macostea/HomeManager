﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SensorListener.Listeners;
using SensorListener.QueueClients;
using Swashbuckle.AspNetCore.Swagger;

namespace SensorListener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sensorServiceURL = Environment.GetEnvironmentVariable("SENSOR_SERVICE_URL") ?? "http://localhost";

            services.AddSingleton<RabbitMQClient>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<RabbitMQClient>>();
                RabbitMQClient client = new RabbitMQClient(Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
                Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE") ?? "SensorsExchange",
                Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? "SensorsQueue",
                logger
                );

                client.RegisterListener(new SensorReadingListener("environment", sensorServiceURL));
                client.RegisterListener(new WeatherReadingListener("weather", sensorServiceURL));
                client.RegisterListener(new NewSensorListener("sensor", sensorServiceURL));

                client.StartListening();

                return client;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SensorService API", Version = "v1" });
            });

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<RabbitMQClient>(); // We need to instantiate this singleton here
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SensorListener API v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
