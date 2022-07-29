using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class BasketItemVM
    {
        public Book Book { get; set; }
        public int Count { get; set; }
    }
}
