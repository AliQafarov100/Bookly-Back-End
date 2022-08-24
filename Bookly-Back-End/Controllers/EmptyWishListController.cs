using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Controllers
{
    public class EmptyWishListController : Controller
    {
        public IActionResult EmptyWishList()
        {
            return View();
        }
    }
}
