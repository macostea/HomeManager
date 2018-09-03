using System;
using System.Collections.Generic;
using System.Text;

namespace HomeManager.SensorListener.Listeners
{
    interface ISensorListener
    {
        string Topic { get; }
        void ProcessMessageAsync(string message);
    }
}
