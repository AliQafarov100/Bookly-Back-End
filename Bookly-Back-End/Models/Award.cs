﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
    }
}
