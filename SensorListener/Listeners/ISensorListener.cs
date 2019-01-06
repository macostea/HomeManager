using System.Threading.Tasks;

namespace SensorListener.Listeners
{
    interface ISensorListener
    {
        string Topic { get; }
        Task<bool> ProcessMessageAsync(string message);
    }
}
