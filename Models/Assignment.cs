using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Assignment
    {
        public Assignment()
        {
            ModuleResult = new HashSet<ModuleResult>();
        }

        public string AssignmentId { get; set; }
        public string AssignmentType { get; set; }
        public string DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<ModuleResult> ModuleResult { get; set; }
    }
}
