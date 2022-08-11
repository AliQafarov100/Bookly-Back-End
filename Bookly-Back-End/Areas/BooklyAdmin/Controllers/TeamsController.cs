using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    public class TeamsController : Controller
    {
        public TeamsController(AppDbContext context)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
