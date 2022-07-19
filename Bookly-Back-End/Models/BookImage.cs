using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public bool? IsMain { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
