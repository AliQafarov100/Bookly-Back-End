using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bookly_Back_End.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }
        public bool IsBest { get; set; }
        public bool IsDailyDeal { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        public int? FormatId { get; set; }
        public Format Format { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookImage> BookImages { get; set; }
        [NotMapped]
        public IFormFile MainImage { get; set; }
        [NotMapped]
        public List<IFormFile> AnotherImages { get; set; }
        [NotMapped]
        public List<int> ImageIds { get; set; }
        [NotMapped]
        public int MainId { get; set; }
        [NotMapped]
        public List<int> AuthorIds { get; set; }
    }
}
