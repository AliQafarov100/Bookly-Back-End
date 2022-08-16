﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class HomeVM
    {
        public Slayd Slayd { get; set; }
        public List<Support> Supports { get; set; }
        public Festival Festival { get; set; }
        public Offer Offer { get; set; }
        public Gift Gift { get; set; }
        public List<Sponsor> Sponsors { get; set; }
        public Author Author { get; set; }
        public List<Book> AllBooks { get; set; }
        public List<Book> AnotherBooks { get; set; }
        public List<Category> Categories { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<Award> Awards { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<AuthorAward> AuthorAwards { get; set; }
        public List<AuthorSocialMedia> AuthorSocialMedias { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Format> Formats { get; set; }
        public Book Book { get; set; }
        public AppUser AppUser { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
