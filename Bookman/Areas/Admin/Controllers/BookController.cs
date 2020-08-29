using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bookman.Models;
using Bookman.Models.DomainModels;
using Bookman.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Bookman.Area.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BookController : Controller
    {
        private BookContext context;
        private List<Binding> bindings;
        private List<Condition> conditions;
        private List<Genre> genres;

        private readonly IHostingEnvironment hostingEnvironment;

        public BookController(BookContext ctx, IHostingEnvironment hostingEnvironment)
        {
            context = ctx;
            this.hostingEnvironment = hostingEnvironment;
            bindings = context.Bindings
                .OrderBy(b => b.Type).ToList();
            conditions = context.Conditions
                .OrderBy(c => c.Status).ToList();
            genres = context.Genres
                .OrderBy(g => g.Name).ToList();
        }

        public RedirectToActionResult Index()
        {
            return RedirectToAction("List");
        }


        [Route("[area]/[controller]s/{id?}")]
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
                    .Include(b => b.Binding)
                    .Include(b => b.Condition)
                    .Include(b => b.Genre)
                    .FirstOrDefault(b => b.BookID == id);
/*
            var model = new BookViewModel
            {
                Book = context.Books
                    .Include(m => m.Binding)
                    .Include(m => m.Condition)
                    .Include(m => m.Genre)
                    .FirstOrDefault(b => b.BookID == id)
            };*/

            return View(book);

        }

        [HttpGet]
        public ViewResult Add()
        {
            Book book = new Book();
            book.Binding = context.Bindings.Find(1);
            book.Condition = context.Conditions.Find(1);
            book.Genre = context.Genres.Find(1);        // prevents validation errors

            // use viewbag to pass action and category data to view
            ViewBag.Action = "Add";
            ViewBag.Bindings = bindings;
            ViewBag.Conditions = conditions;
            ViewBag.Genres = genres;

            return View("AddUpdate", book);
        }


        [HttpGet]
        public ViewResult Update(int id)
        {
            Book book = context.Books
                .Include(b => b.Binding)
                .Include(b => b.Condition)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.BookID == id);

            ViewBag.Action = "Update";
            ViewBag.Bindings = bindings;
            ViewBag.Conditions = conditions;
            ViewBag.Genres = genres;

            return View("AddUpdate", book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (book.CoverImage != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = book.CoverImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    book.CoverImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                book.ImageString = uniqueFileName;

                /*Book newBook = new Book
                {
                    Title = book.Title,
                    AuthorFirstName = book.AuthorFirstName,
                    AuthorLastName = book.AuthorLastName,
                    BindingID = book.BindingID,
                    ConditionID = book.ConditionID,
                    GenreID = book.GenreID,
                    PageCount = book.PageCount,
                    ImageString = uniqueFileName
                };*/

                if (book.BookID == 0) // new book
                {
                    context.Books.Add(book);
                }
                else
                {
                    context.Update(book);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = "Save";
                ViewBag.Genres = genres;
                return View("AddUpdate", book);
            }
        }


        [HttpGet]
        public ViewResult Delete(int id)
        {
            Book book = context.Books.FirstOrDefault(b => b.BookID == id);

            return View(book);
        }


        [HttpPost]
        public RedirectToActionResult Delete(Book book)
        {
            context.Books.Remove(book);
            context.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
