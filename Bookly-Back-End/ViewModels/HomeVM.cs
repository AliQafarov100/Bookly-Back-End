using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class HomeVM
    {
        public Slayd Slayd { get; set; }
        public IQueryable<Support> Supports { get; set; }
        public Festival Festival { get; set; }
        public Offer Offer { get; set; }
        public Gift Gift { get; set; }
        public IQueryable<Sponsor> Sponsors { get; set; }
        public Author Author { get; set; }
        public IQueryable<Book> AllBooks { get; set; }
        public IQueryable<Book> AnotherBooks { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public IQueryable<Award> Awards { get; set; }
        public IQueryable<SocialMedia> SocialMedias { get; set; }
        public IQueryable<AuthorAward> AuthorAwards { get; set; }
        public IQueryable<AuthorSocialMedia> AuthorSocialMedias { get; set; }
        public IQueryable<Discount> Discounts { get; set; }
        public IQueryable<Blog> Blogs { get; set; }
        public IQueryable<Format> Formats { get; set; }
        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
        public IQueryable<BasketItem> BasketItems { get; set; }
       
    }
}
