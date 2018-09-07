using System;
using System.Data.Common;

namespace HomeManager.Common.Models
{
    public interface ITimescaleRepresentable
    {
        void FromTimescaleReader(DbDataReader reader);
    }
}
