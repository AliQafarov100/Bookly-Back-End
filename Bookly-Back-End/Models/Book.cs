using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal? OldPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookFormat> BookFormats { get; set; }
        public List<BookLanguage> BookLanguages { get; set; }
        public List<BookImage> BookImages { get; set; }
    }
}
