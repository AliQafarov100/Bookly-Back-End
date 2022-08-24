using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.Interfaces
{
    public interface IQuery
    {
        IQueryable<Slayd> Slayds { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Festival> Festivals { get; }
        IQueryable<Gift> Gifts { get; }
        IQueryable<Sponsor> Sponsors { get; }
        IQueryable<Offer> Offers { get; }
        IQueryable<Support> Supports { get; }
        IQueryable<Author> Authors { get; }
        IQueryable<Blog> Blogs { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<Format> Formats { get; }
        IQueryable<FilteringPrice> FilteringPrices { get;}
        IQueryable<Book> Books { get; }
    }
}
