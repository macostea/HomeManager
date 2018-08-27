using System;
namespace HomeManagerWeb.Models
{
    public interface ISensorReading<T>
    {
        DateTime Time { get; }
        T Reading { get; }
    }
}
