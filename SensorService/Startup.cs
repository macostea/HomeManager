using Common.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace SensorService
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
            services.AddTransient<IRepository<Sensor>, SensorsRepository<Sensor>>();
            services.AddTransient<IRepository<TemperatureSensorReading>, SensorsRepository<TemperatureSensorReading>>();
            services.AddTransient<IRepository<HumiditySensorReading>, SensorsRepository<HumiditySensorReading>>();
            services.AddTransient<IRepository<WeatherSensorReading>, SensorsRepository<WeatherSensorReading>>();
            var connectionString = Configuration.GetConnectionString("SensorsContext");
            services.AddEntityFrameworkNpgsql().AddDbContext<SensorsContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("SensorService")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SensorService API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SensorService API v1");
            });
            app.UseMvc();
        }
    }
}
