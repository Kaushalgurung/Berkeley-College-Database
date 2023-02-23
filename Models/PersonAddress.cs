using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class PersonAddress
    {
        public string PersonId { get; set; }
        public string AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
