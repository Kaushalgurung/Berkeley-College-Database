using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Module
    {
        public Module()
        {
            StudentModule = new HashSet<StudentModule>();
            TeacherModule = new HashSet<TeacherModule>();
        }

        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public decimal CreditHours { get; set; }

        public virtual ICollection<StudentModule> StudentModule { get; set; }
        public virtual ICollection<TeacherModule> TeacherModule { get; set; }
    }
}
