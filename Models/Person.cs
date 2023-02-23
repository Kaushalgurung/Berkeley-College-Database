using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace berkeley_collegee.Models
{
    public partial class Person
    {
        public Person()
        {
            PersonAddress = new HashSet<PersonAddress>();
        }

        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
        [NotMapped]
        public virtual Address Address{get; set;}
    }
}
