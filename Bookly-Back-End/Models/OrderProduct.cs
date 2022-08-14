using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BookId { get; set; }
        public string AppUserId { get; set; }
        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
        [Required, Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
