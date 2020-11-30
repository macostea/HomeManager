using Dapper.FluentMap.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Mappings
{
    // This name... I know...
    public class HomeyMappingMap : EntityMap<HomeyMapping>
    {
        public HomeyMappingMap()
        {
            Map(u => u.HumTopic).ToColumn("hum_topic");
            Map(u => u.TempTopic).ToColumn("temp_topic");
            Map(u => u.MotionTopic).ToColumn("motion_topic");
        }
    }
}
