using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDashboard.Infrastructure.Services
{
    public class CommonServices : ICommonServices
    {
        public CommonServices(IContextService contextService, INavigationService navigationService, ILogService logService)
        {
            ContextService = contextService;
            NavigationService = navigationService;
            LogService = logService;

        }
        public IContextService ContextService { get; }

        public INavigationService NavigationService { get; }

        public ILogService LogService { get; }
    }
}
