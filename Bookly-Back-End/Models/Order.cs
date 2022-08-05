using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool? Status { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
