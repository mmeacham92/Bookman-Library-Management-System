using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Bookman.Models.ViewModels
{
    public class BookCreateViewModel
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        [Required(ErrorMessage = "Please select a binding type.")]
        public int BindingID { get; set; }
        public Binding Binding { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please selet a condition status.")]
        public int ConditionID { get; set; }
        public Condition Condition { get; set; }

        [Required(ErrorMessage = "Please enter a page count.")]
        public int PageCount { get; set; }

        public IFormFile CoverImage { get; set; }
    }
}
