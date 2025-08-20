using System.Collections.Generic;

namespace Books_Api.ViewModels
{
    public class PagedDTO<T>
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
        public double Pages { get; set; }
    }
}