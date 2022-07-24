using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
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
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
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
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
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

        public async Task<IActionResult> Read(int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();

            Book book = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
               Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
            Book book = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(Book book, int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
            Book exsisted = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);

            if (exsisted == null) return NotFound();
            if (book.MainImage != null)
            {
                BookImage mainImage = new BookImage
                {
                    ImagePath = await book.MainImage.FileCreate(_env.WebRootPath, @"assets\Image\Library"),
                    IsMain = true,
                    BookId = exsisted.Id
                };
                exsisted.BookImages.Add(mainImage);

                BookImage removableImage = exsisted.BookImages.FirstOrDefault(i => i.IsMain == true);

                exsisted.BookImages.Remove(removableImage);

                FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Library", removableImage.ImagePath);

            }

            if (book.ImageIds == null && book.AnotherImages == null)
            {
                ModelState.AddModelError("", "You can not delete all images without adding another(-s) image");
                return View(exsisted);
            }

            List<BookImage> removebleImages = exsisted.BookImages.Where(i => i.IsMain == false && !book.ImageIds.Contains(i.Id)).ToList();
            List<BookFormat> removebleFormats = exsisted.BookFormats.Where(bf => !book.FormatIds.Contains(bf.FormatId)).ToList();
            List<BookLanguage> removableLanguage = exsisted.BookLanguages.Where(bl => !book.LanguageIds.Contains(bl.LanguageId)).ToList();
            List<BookAuthor> removableAuthors = exsisted.BookAuthors.Where(ba => !book.AuthorIds.Contains(ba.AuthorId)).ToList();
            

            exsisted.BookImages.RemoveAll(p => removebleImages.Any(ri => ri.Id == p.Id));
            exsisted.BookFormats.RemoveAll(b => removebleFormats.Any(rf => rf.Id == b.Id));
            exsisted.BookLanguages.RemoveAll(b => removableLanguage.Any(bl => bl.Id == b.Id));
            exsisted.BookAuthors.RemoveAll(b => removableAuthors.Any(ba => ba.Id == b.Id));
            

            foreach (var image in removebleImages)
            {
                FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Library", image.ImagePath);
            }
            foreach (var formatId in book.FormatIds)
            {
                BookFormat existedFormat = exsisted.BookFormats.FirstOrDefault(bf => bf.FormatId == formatId);
                if (existedFormat == null)
                {
                    BookFormat bookFormat = new BookFormat
                    {
                        BookId = exsisted.Id,
                        FormatId = formatId
                    };
                    exsisted.BookFormats.Add(bookFormat);
                }
            }
            foreach(var languageId in book.LanguageIds)
            {
                BookLanguage existedLanguage = exsisted.BookLanguages.FirstOrDefault(bl => bl.LanguageId == languageId);
                if(existedLanguage == null)
                {
                    BookLanguage bookLanguage = new BookLanguage
                    {
                        BookId = exsisted.Id,
                        LanguageId = languageId
                    };

                    exsisted.BookLanguages.Add(bookLanguage);
                }   
            }
            foreach(var authorId in book.AuthorIds)
            {
                BookAuthor existedAuthor = exsisted.BookAuthors.FirstOrDefault(a => a.AuthorId == authorId);
                if(existedAuthor == null)
                {
                    BookAuthor bookAuthor = new BookAuthor
                    {
                        BookId = exsisted.Id,
                        AuthorId = authorId
                    };
                    exsisted.BookAuthors.Add(bookAuthor);
                }
            }

            if (book.AnotherImages != null)
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

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
            Book existedBook = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);


            if (existedBook == null) return NotFound();

            return View(existedBook);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            ViewBag.Formats = await _context.Formats.ToListAsync();
            ViewBag.Languages = await _context.Languages.ToListAsync();
            ViewBag.Authors = await _context.Authors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
            Book existedBook = await _context.Books.Include(f => f.BookFormats).Include(i => i.BookImages).
                Include(a => a.BookAuthors).Include(l => l.BookLanguages).FirstOrDefaultAsync(b => b.Id == id);

            if (existedBook == null) return NotFound();

            _context.Remove(existedBook);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
