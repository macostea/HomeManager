using System;
using Dapper.FluentMap.Mapping;
using Domain.Entities;

namespace Common.Mappings
{
    public class SensorMap : EntityMap<Sensor>
    {
        public SensorMap()
        {
            Map(u => u.RoomId).ToColumn("room_id");
        }
    }
}
