using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeacherModule = new HashSet<TeacherModule>();
        }

        public string TeacherId { get; set; }
        public string Qualification { get; set; }

        public virtual Person TeacherNavigation { get; set; }
        public virtual ICollection<TeacherModule> TeacherModule { get; set; }
    }
}
