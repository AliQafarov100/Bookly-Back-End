using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public List<City> Cities { get; set; }
        public Country Country { get; set; }
        public List<Delivery> Deliveries { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Book> Books { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}
