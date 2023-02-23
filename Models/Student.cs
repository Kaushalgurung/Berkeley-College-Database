using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Student
    {
        public Student()
        {
            FeePayment = new HashSet<FeePayment>();
            StudentAttendence = new HashSet<StudentAttendence>();
            StudentModule = new HashSet<StudentModule>();
        }

        public string StudentId { get; set; }
        public string EnrolledYear { get; set; }

        public virtual Person StudentNavigation { get; set; }
        public virtual ICollection<FeePayment> FeePayment { get; set; }
        public virtual ICollection<StudentAttendence> StudentAttendence { get; set; }
        public virtual ICollection<StudentModule> StudentModule { get; set; }

    }
}
