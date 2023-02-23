using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class TeacherModule
    {
        public string TeacherId { get; set; }
        public string ModuleCode { get; set; }

        public virtual Module ModuleCodeNavigation { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
