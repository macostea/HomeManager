using IOTDashboard.Infrastructure.Logging;
using IOTDashboard.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace IOTDashboard
{
    public class Startup
    {
        static private readonly ServiceCollection _serviceCollection = new ServiceCollection();
        static public async Task ConfigureAsync()
        {
            ServiceLocator.Configure(_serviceCollection);
            var logService = ServiceLocator.Current.GetService<ILogService>();

            await logService.WriteAsync(LogType.Information, "Startup", "Configuration", "Application Start", $"Application started.");
        }
    }
}
