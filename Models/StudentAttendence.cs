using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class StudentAttendence
    {
        public string AttendenceId { get; set; }
        public string StudentId { get; set; }
        public decimal Attendence { get; set; }

        public virtual Student Student { get; set; }
    }
}
