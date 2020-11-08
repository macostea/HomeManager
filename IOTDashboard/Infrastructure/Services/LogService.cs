using IOTDashboard.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDashboard.Infrastructure.Services
{
    public class LogService : ILogService
    {
        public Task WriteAsync(LogType type, string source, string action, string message, string description)
        {
            return Task.CompletedTask;
        }

        public Task WriteAsync(LogType type, string source, string action, Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
