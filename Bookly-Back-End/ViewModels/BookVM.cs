using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class BookVM
    {
        public List<Format> Formats { get; set; }
        public List<Language> Languages { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}
