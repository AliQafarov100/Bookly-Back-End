using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class AuthorSocialMedia
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}
