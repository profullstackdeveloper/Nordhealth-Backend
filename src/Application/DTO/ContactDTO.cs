using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
    }

    public enum ContactType
    {
        Phone,
        Mail,
        Web
    }
}
