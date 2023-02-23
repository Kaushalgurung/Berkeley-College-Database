using System;
using System.Collections.Generic;

namespace berkeley_collegee.Models
{
    public partial class Address
    {
        public Address()
        {
            PersonAddress = new HashSet<PersonAddress>();
        }

        public string AddressId { get; set; }
        public string Country { get; set; }
        public string ProvinceOrState { get; set; }
        public string City { get; set; }
        public decimal? StreetNo { get; set; }
        public string StreetName { get; set; }

        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
