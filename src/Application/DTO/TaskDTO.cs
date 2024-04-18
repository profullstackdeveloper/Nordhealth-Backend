﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Solved { get; set; }
    }
}
