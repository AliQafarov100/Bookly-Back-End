using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class FilteringPrice
    {
        public int Id { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
    }
}
