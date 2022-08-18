using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class FAQ
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Response { get; set; }
    }
}
