using System;
using System.Data.Common;

namespace Common.Models
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}
