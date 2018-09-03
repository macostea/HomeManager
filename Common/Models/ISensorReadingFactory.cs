using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Common.Models
{
    public interface ISensorReadingFactory
    {
        ISensorReading<double> NewDouble();
    }
}
