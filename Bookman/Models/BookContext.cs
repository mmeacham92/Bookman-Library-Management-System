using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bookman.Models.DomainModels;

namespace Bookman.Models
{
    public class BookContext : IdentityDbContext<ApplicationUser>
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {

        }

        public DbSet<Binding> Bindings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Binding data
            modelBuilder.Entity<Binding>().HasData(
                new Binding { BindingID = 1, Type = "Hardback" },
                new Binding { BindingID = 2, Type = "Paperback" }
                );

            // Seed Condition data
            modelBuilder.Entity<Condition>().HasData(
                new Condition { ConditionID = 1, Status = "New" },
                new Condition { ConditionID = 2, Status = "Like New" },
                new Condition { ConditionID = 3, Status = "Used" },
                new Condition { ConditionID = 4, Status = "Very Worn" },
                new Condition { ConditionID = 5, Status = "Bad" }
                );

            // Seed Genre Data
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, Name = "Memoir" },
                new Genre { GenreID = 2, Name = "Educational" },
                new Genre { GenreID = 3, Name = "Fantasy" },
                new Genre { GenreID = 4, Name = "Science Fiction" },
                new Genre { GenreID = 5, Name = "Biography" },
                new Genre { GenreID = 6, Name = "Classical" },
                new Genre { GenreID = 7, Name = "Horror" }
                );

            //Seed Book Data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookID = 1,
                    Title = "The Name of the Wind",
                    AuthorFirstName = "Patrick",
                    AuthorLastName = "Rothfuss",
                    BindingID = 2,
                    ConditionID = 2,
                    GenreID = 3,
                    ImageString = "the-name-of-the-wind.jpg",
                    PageCount = 662
                });
        }
    }
}
