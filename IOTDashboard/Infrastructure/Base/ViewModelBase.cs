using IOTDashboard.Infrastructure.Logging;
using IOTDashboard.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDashboard.Infrastructure.Base
{
    public class ViewModelBase : ObservableObject
    {
        public ViewModelBase(ICommonServices commonServices)
        {
            ContextService = commonServices.ContextService;
            NavigationService = commonServices.NavigationService;
            LogService = commonServices.LogService;
        }

        public IContextService ContextService { get; }
        public INavigationService NavigationService { get; }
        public ILogService LogService { get; }

        public bool IsMainView => ContextService.IsMainView;
        virtual public string Title => String.Empty;

        public async void LogInformation(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Information, source, action, message, description);
        }

        public async void LogWarning(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Warning, source, action, message, description);
        }

        public void LogException(string source, string action, Exception exception)
        {
            LogError(source, action, exception.Message, exception.ToString());
        }
        public async void LogError(string source, string action, string message, string description)
        {
            await LogService.WriteAsync(LogType.Error, source, action, message, description);
        }
    }
}
