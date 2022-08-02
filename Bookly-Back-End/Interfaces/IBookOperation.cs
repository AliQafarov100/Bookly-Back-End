using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.Interfaces
{
    public interface IBookOperation
    {
        IQueryable<Book> Books { get; }

        IQueryable<Book> GetBookByCategory(int? category,int? author,string highTolow);
    }
}
