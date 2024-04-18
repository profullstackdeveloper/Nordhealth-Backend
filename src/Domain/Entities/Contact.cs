using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Domain.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        // Foreign key to the Customer
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public enum ContactType
    {
        Phone,
        Mail,
        Web
    }
}
