using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class ModuleResult
    {
        public string ResultId { get; set; }
        public string AssignmentId { get; set; }
        public string ModuleCode { get; set; }
        public string StudentId { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }

        public virtual Assignment Assignment { get; set; }
        public virtual StudentModule StudentModule { get; set; }
    }
}
