using System;
using System.Collections.Generic;

namespace Books_Business.Core.Models
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public string Query { get; set; }
        public double Pages => Math.Ceiling((double)Total / Size);
    }
}