using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bookman.Models;
using Bookman.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;

namespace Bookman.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GenreController : Controller
    {
        private BookContext context;

        public GenreController(BookContext ctx)
        {
            context = ctx;
        }

        public RedirectToActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Route("[area]/[controller]s/[action]/{id?}")]
        public ViewResult List()
        {
            List<Genre> genres = context.Genres
                .OrderBy(g => g.GenreID).ToList();

            return View(genres);
        }


        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddUpdate", new Genre());
        }


        [HttpGet]
        public ViewResult Update(int id)
        {
            ViewBag.Action = "Update";
            var genre = context.Genres.Find(id);

            return View("AddUpdate", genre);
        }


        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.GenreID == 0)
                {
                    context.Genres.Add(genre);
                }
                else
                {
                    context.Genres.Update(genre);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = "Save";
                return View("AddUpdate");
            }
        }


        [HttpGet]
        public ViewResult Delete(int id)
        {
            Genre genre = context.Genres.Find(id);

            return View(genre);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Genre genre)
        {
            context.Genres.Remove(genre);
            context.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
