using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
