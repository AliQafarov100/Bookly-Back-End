using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<AuthorAward> AuthorAwards { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
