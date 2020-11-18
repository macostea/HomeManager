using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HomeyMapping : EntityBase
    {
        public string TempTopic { get; set; }
        public string HumTopic { get; set; }
        public string MotionTopic { get; set; }
    }
}
