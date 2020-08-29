using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookman.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public string ActiveGenre { get; set; }
    }
}
