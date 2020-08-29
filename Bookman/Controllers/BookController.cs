using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bookman.Models.ViewModels;
using Bookman.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Bookman.Controllers
{
    
    public class BookController : Controller
    {
        private BookContext context;

        public BookController(BookContext ctx)
        {
            context = ctx;
        }

        public RedirectToActionResult Index()
        {
            return RedirectToAction("List", "Book");
        }


        [Route("[controller]s/{id?}")]
        public ViewResult List(string id = "All")
        {
            var model = new BookListViewModel
            {
                ActiveGenre = id,
                Genres = context.Genres.ToList()
            };

            IQueryable<Book> query = context.Books;

            if (id != "All")
                query = query.Where(b => b.Genre.Name.ToLower() == id.ToLower());

            model.Books = query
                        .Include(m => m.Binding)
                        .Include(m => m.Condition)
                        .Include(m => m.Genre)
                        .OrderBy(b => b.Title).ToList();

            return View(model);
        }


        public ViewResult Details(int id)
        {
            Book book = context.Books
                .Include(b => b.Genre)
                .Include(b => b.Condition)
                .Include(b => b.Binding)
                .FirstOrDefault(b => b.BookID == id);

            return View(book);
        }
    }
}