using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool Solved { get; set; }

        // Foreign key to the Customer
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
