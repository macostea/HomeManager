using System;
namespace HomeManager.Models
{
    public interface ISensorReading<T>
    {
        DateTime Time { get; }
        T Reading { get; }
    }
}
