using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class TaskCreateDTO
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Solved { get; set; }
    }

    public class TaskUpdateDTO
    {
        public string? Description { get; set; }
        public bool? Solved { get; set; }
    }
}
