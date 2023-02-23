using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Department
    {
        public Department()
        {
            Assignment = new HashSet<Assignment>();
            FeePayment = new HashSet<FeePayment>();
        }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentContact { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual ICollection<FeePayment> FeePayment { get; set; }
    }
}
