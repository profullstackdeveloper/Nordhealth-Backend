using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.PaginatedResult
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
