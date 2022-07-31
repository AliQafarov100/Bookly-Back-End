using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
