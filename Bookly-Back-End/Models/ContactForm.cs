using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress), StringLength(40)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber), StringLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
