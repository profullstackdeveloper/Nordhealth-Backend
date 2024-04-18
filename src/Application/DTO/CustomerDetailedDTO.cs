using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CustomerDetailedDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public List<ContactDTO> Contacts { get; set; }
        public List<TaskDTO> Tasks { get; set; }
    }
}
