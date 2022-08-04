using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<City> Cities { get; set; }
    }
}
