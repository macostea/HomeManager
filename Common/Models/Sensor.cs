using System;
using System.Data.Common;

namespace HomeManager.Common.Models
{
    public class Sensor : ISensor
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public String Type { get; set; }

        public Sensor()
        {
        }

        public void FromTimescaleReader(DbDataReader reader)
        {
            this.Id = reader.GetInt32(0);
            this.Name = reader.GetString(1);
            this.Location = reader.GetString(2);
            this.Type = reader.GetString(3);
        }
    }
}
