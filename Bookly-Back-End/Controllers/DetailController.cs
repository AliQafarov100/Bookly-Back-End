using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Braintree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Discount = Bookly_Back_End.Models.Discount;

namespace Bookly_Back_End.Controllers
{
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBraintreeService _braintreeService;

        public DetailController(AppDbContext context,UserManager<AppUser> userManager,IBraintreeService braintreeService)
        {
            _context = context;
            _userManager = userManager;
            _braintreeService = braintreeService;
        }
        public async Task<ActionResult> Details(int id)
        {
            Book book = await _context.Books.Include(b => b.BookImages).Include(b => b.BookAuthors).Include(c => c.Category).FirstOrDefaultAsync(b => b.Id == id);
            List<Discount> discounts = await _context.Discounts.ToListAsync();
            List<BookAuthor> bookAuthors = await _context.BookAuthors.ToListAsync();
            List<Author> authors = await _context.Authors.Include(a => a.BookAuthors).ToListAsync();
            List<Format> formats = await _context.Formats.ToListAsync();
            List<Language> languages = await _context.Languages.ToListAsync();
            List<Book> books = await _context.Books.Include(b => b.BookImages).Include(b => b.BookAuthors).ToListAsync();
           
            BookVM model = new BookVM
            {
                Book = book,
                Discounts = discounts,
                BookAuthors = bookAuthors,
                Authors = authors,
                AnotherBooks = books
            };
            return View(model);
        }

        public async Task<IActionResult> Purchase(int? id)
        {
            if (id is null && id == 0) return NotFound();

            Book book = await _context.Books.Include(b => b.BookAuthors).Include(b => b.BookImages)
                .Include(b => b.Discount).FirstOrDefaultAsync(b => b.Id == id);


            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            var data = new BookPurchaseVM
            {
                Id = book.Id,
                Description = book.Description,
                BookImages = book.BookImages.Where(b => b.IsMain == true).ToList(),
                BookAuthors = book.BookAuthors.Where(b => b.BookId == book.Id).ToList(),
                Name = book.Name,
                Price = book.DiscountId == null ? book.Price : book.Price - ((book.Price * book.Discount.DiscountPercent) / 100),
                Nonce = ""
            };

            return View(data);
        }

        public IActionResult Create(BookPurchaseVM model)
        {
            var gateway = _braintreeService.GetGateway();

            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal("250"),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                return View("Success");
            }
            else
            {
                return View("Failure");
            }
        }
    }
}
