using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDashboard.Infrastructure.Services
{
    public interface IContextService
    {
        int MainViewID { get; }
        int ContextID { get; }
        bool IsMainView { get; }
        void Initialize(object dispatcher, int contextId, bool isMainView);
        Task RunAsync(Action action);
    }
}
