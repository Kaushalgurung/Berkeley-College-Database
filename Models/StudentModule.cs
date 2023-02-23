using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class StudentModule
    {
        public StudentModule()
        {
            ModuleResult = new HashSet<ModuleResult>();
        }

        public string StudentId { get; set; }
        public string ModuleCode { get; set; }

        public virtual Module ModuleCodeNavigation { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<ModuleResult> ModuleResult { get; set; }
    }
}
