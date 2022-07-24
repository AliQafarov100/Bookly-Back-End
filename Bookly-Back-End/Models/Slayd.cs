using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Models
{
    public class Slayd
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
