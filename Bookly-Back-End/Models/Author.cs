using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public List<AuthorSocialMedia> AuthorSocialMedias { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public List<int> AwardIds { get; set; }
        [NotMapped]
        public List<int> SocialMediaIds { get; set; }
        [NotMapped]
        public string Best { get; set; }
    }
}
