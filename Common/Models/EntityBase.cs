using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Common.Models
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; protected set; }
    }
}
