using System.Data;
using Common.Mappings;
using Common.Repository;
using Common.SensorListenerAPI;
using Dapper;
using Dapper.FluentMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SensorService.Validation;
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
            services.AddMvc(config =>
            {
                config.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });
            var connectionString = Configuration.GetConnectionString("SensorsContext");
            services.AddTransient<IDbConnection>(s => new NpgsqlConnection(connectionString));
            services.AddTransient<IHomeRepository, HomeRepository>();
            services.AddTransient<ISensorListenerAPI>(s => new SensorListenerAPI(this.Configuration["SENSOR_LISTENER"]));

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new WeatherMap());
                config.AddMap(new SensorMap());
                config.AddMap(new HomeyMappingMap());
            });
            SqlMapper.AddTypeHandler(new UriTypeHandler());

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
