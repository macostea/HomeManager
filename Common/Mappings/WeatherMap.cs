using System;
using Dapper.FluentMap.Mapping;
using Domain.Entities;

namespace Common.Mappings
{
    public class WeatherMap : EntityMap<Weather>
    {
        public WeatherMap()
        {
            Map(u => u.MinimumTemperature).ToColumn("minimum_temperature");
            Map(u => u.MaximumTemperature).ToColumn("maximum_temperature");
            Map(u => u.ConditionCode).ToColumn("condition_code");
            Map(u => u.IconURL).ToColumn("icon_url");
        }
    }
}
