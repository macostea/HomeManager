using System.Threading.Tasks;

namespace SensorListener.Listeners
{
    public interface ISensorListener
    {
        string Topic { get; }
        Task<bool> ProcessMessageAsync(string message);
    }
}
