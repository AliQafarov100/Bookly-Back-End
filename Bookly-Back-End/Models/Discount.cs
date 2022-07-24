﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public byte DiscountPercent { get; set; }
        public List<Book> Books { get; set; }
    }
}
