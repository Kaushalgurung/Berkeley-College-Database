using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class FeePayment
    {
        public decimal InvoiceNo { get; set; }
        public string StudentId { get; set; }
        public string DepartmentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        public virtual Department Department { get; set; }
        public virtual Student Student { get; set; }
    }
}
