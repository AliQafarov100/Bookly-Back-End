using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class BookVM
    {
        public IQueryable<Format> Formats { get; set; }
        public IQueryable<Language> Languages { get; set; }
        public IQueryable<Author> Authors { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public IQueryable<Book> AllBooks { get; set; }
        public IQueryable<Book> AnotherBooks { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public IQueryable<Discount> Discounts { get; set; }
        public IQueryable<FilteringPrice> FilteringPrices { get; set; }
        public Book Book { get; set; }
        public int Counter { get; set; }
    }
}
