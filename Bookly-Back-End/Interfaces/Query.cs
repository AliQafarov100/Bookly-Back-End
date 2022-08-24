using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.Interfaces
{
    public class Query : IQuery
    {
        private readonly AppDbContext _context;

        public Query(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Slayd> Slayds => _context.Slayds.AsQueryable();

        public IQueryable<Category> Categories => _context.Categories.AsQueryable();

        public IQueryable<Festival> Festivals => _context.Festivals.AsQueryable();

        public IQueryable<Gift> Gifts => _context.Gifts.AsQueryable();

        public IQueryable<Sponsor> Sponsors => _context.Sponsors.AsQueryable();

        public IQueryable<Offer> Offers => _context.Offers.AsQueryable();

        public IQueryable<Support> Supports => _context.Supports.AsQueryable();

        public IQueryable<Author> Authors => _context.Authors.AsQueryable();

        public IQueryable<Blog> Blogs => _context.Blogs.AsQueryable();
        public IQueryable<Format> Formats => _context.Formats.AsQueryable();
        public IQueryable<Language> Languages => _context.Languages.AsQueryable();
        public IQueryable<FilteringPrice> FilteringPrices => _context.FilteringPrices.AsQueryable();
        public IQueryable<Book> Books => _context.Books.AsQueryable();
    }
}
