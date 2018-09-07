using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeManager.Common.Models
{
    public interface ISensor : ITimescaleRepresentable
    {
        int Id { get; }
        String Name { get; }
        String Location { get; }
        String Type { get; }
    }
}
