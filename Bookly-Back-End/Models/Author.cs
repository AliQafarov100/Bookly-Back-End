using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Comments { get; set; }
        public bool IsBest { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<AuthorAward> AuthorAwards { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
