﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookFormat> BookFormats { get; set; }
    }
}
