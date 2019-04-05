using Dashboard.Store;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Dashboard
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DashboardStore>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.UseLocalTimeZone();
            app.AddComponent<App>("app");
        }
    }
}
