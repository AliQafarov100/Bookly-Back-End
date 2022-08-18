using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class BookPurchaseVM : Book
    {
        public string Nonce { get; set; }
    }
}
