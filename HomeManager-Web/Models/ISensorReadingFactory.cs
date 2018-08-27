using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManagerWeb.Models
{
    public interface ISensorReadingFactory
    {
        ISensorReading<double> NewDouble();
    }
}
