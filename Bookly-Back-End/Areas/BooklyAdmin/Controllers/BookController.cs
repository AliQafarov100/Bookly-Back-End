using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _context.Books.Include(a => a.BookAuthors).Include(f => f.BookFormats).
                Include(l => l.BookLanguages).Include(i => i.BookImages).ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Book book)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) return View();
            if (book.MainImage == null || book.AnotherImages == null)
            {
                ModelState.AddModelError("", "Please choose main image or another image file");
                return View();
            }
            if (!book.MainImage.IsOkay(1))
            {
                ModelState.AddModelError("MainImage", "Size of image must be 1Mb!");
                return View();
            }
            foreach (var image in book.AnotherImages)
            {
                if (!image.IsOkay(1))
                {
                    ModelState.AddModelError("AnotherImage", "Size of image muste be 1MB!");
                    return View();
                }
            }

            book.BookImages = new List<BookImage>();

            BookImage mainImage = new BookImage
            {
                IsMain = true,
                ImagePath = await book.MainImage.FileCreate(_env.WebRootPath, @"assets\Image\Library"),
                Book = book
            };

            book.BookImages.Add(mainImage);

            foreach (var image in book.AnotherImages)
            {
                BookImage anotherImage = new BookImage
                {
                    IsMain = false,
                    ImagePath = await image.FileCreate(_env.WebRootPath, @"assets\Image\Library"),
                    Book = book
                };

                book.BookImages.Add(anotherImage);
            }

            book.BookFormats = new List<BookFormat>();

            foreach (var id in book.FormatIds)
            {
                BookFormat format = new BookFormat
                {
                    Book = book,
                    FormatId = id
                };

                book.BookFormats.Add(format);
            }

            book.BookAuthors = new List<BookAuthor>();

            foreach (var id in book.AuthorIds)
            {
                BookAuthor author = new BookAuthor
                {
                    Book = book,
                    AuthorId = id
                };

                book.BookAuthors.Add(author);
            }

            book.BookLanguages = new List<BookLanguage>();

            foreach (var id in book.LanguageIds)
            {
                BookLanguage language = new BookLanguage
                {
                    Book = book,
                    LanguageId = id
                };

                book.BookLanguages.Add(language);
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Book book = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(Book book,int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Book exsisted = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);

            if (exsisted == null) return NotFound();

            if(book.ImageIds == null && book.AnotherImages == null)
            {
                ModelState.AddModelError("", "You can not delete all images without adding another image");
                return View(exsisted);
            }

            List<BookImage> removebleImage = exsisted.BookImages.Where(i => i.IsMain == false && !book.ImageIds.Contains(i.Id)).ToList();

            exsisted.BookImages.RemoveAll(p => removebleImage.Any(ri => ri.Id == p.Id));

            foreach(var image in removebleImage)
            {
                FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Library", image.ImagePath);
            }

           if(book.AnotherImages != null)
            {
                foreach (var image in book.AnotherImages)
                {
                    BookImage anotherImage = new BookImage
                    {
                        ImagePath = await image.FileCreate(_env.WebRootPath, @"assets\Image\Library"),
                        BookId = exsisted.Id,
                        IsMain = false
                    };

                    exsisted.BookImages.Add(anotherImage);
                }
            }

            _context.Entry(exsisted).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
