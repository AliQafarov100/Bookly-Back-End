using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class OrderVM
    {
        [Required]
        [StringLength(maximumLength: 20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Address { get; set; }     
        public string Message { get; set; }
        public Country Country { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
