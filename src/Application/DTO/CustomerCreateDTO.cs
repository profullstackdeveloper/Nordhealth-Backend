using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CustomerCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class CustomerUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
