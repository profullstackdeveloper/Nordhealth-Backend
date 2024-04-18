using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ContactCreateDTO
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }

    public class ContactUpdateDTO 
    {
        public ContactType Type { get; set; }
        public string? Value { get; set; }
    }

}
