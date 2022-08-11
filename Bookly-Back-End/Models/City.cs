﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public List<Order> Orders { get; set; }
    }
}