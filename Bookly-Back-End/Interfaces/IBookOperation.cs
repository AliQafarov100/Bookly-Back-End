using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Helpers;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.Interfaces
{
    public interface IBookOperation
    {
        IQueryable<BookAuthor> BookAuthors { get; }
        
        IQueryable<BookAuthor> GetBookByFilter(string category,string author, string sortBy,
            int? minPrice,int? maxPrice,string language,string format);
        IQueryable<BookAuthor> GetBookByCategory(string category);
        IQueryable<BookAuthor> GetBookBySearch(string searching);
        
        
    }
}
