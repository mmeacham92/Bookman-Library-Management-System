using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookman.Models.ViewModels
{
    public class BookListViewModel : BookViewModel
    {
        public List<Book> Books { get; set; }

        private List<Genre> genres;
        public List<Genre> Genres
        {
            get => genres;
            set
            {
                genres = new List<Genre>
                {
                    new Genre { GenreID = 8, Name = "All" }
                };
                genres.AddRange(value);
            }
        }

        public string CheckActiveGenre(string g) =>
            g.ToLower() == ActiveGenre.ToLower() ? "active" : "";
    }
}
