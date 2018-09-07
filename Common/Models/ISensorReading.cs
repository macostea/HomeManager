using System;

namespace HomeManager.Common.Models
{
    public interface ISensorReading<T> : ITimescaleRepresentable
    {
        DateTime Time { get; }
        T Reading { get; }
    }
}
