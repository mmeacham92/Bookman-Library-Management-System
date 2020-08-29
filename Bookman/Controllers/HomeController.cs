using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookman.Models;
using Microsoft.EntityFrameworkCore;
using Bookman.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;

namespace Bookman.Controllers
{

    public class HomeController : Controller
    {
        private BookContext context;

        public HomeController(BookContext ctx)
        {
            context = ctx;
        }

        public ViewResult Index()
        {
            Random rand = new Random();
            var books = context.Books.ToList();
            Book randomBook = books[rand.Next(0, books.Count())];

            return View(randomBook);
        }

        public ViewResult About()
        {
            return View();
        }
    }
}
