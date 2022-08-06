using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Mvc;



namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;

        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
