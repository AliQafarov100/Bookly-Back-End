using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; }
    }
}
