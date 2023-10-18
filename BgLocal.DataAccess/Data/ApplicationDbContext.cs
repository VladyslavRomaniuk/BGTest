using BgLocal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BgLocal.DataAccess.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Alexander", Surname = "Pushkin", BirthDate = new DateOnly(1799, 06, 06) },
                new Author { Id = 2, Name = "Lev", Surname = "Tolstoy", BirthDate = new DateOnly(1828, 09, 09) },
                new Author { Id = 3, Name = "Ivan", Surname = "Turgenev", BirthDate = new DateOnly(1818, 11, 09) },
                new Author { Id = 4, Name = "Nikolay", Surname = "Gogol", BirthDate = new DateOnly(1809, 04, 01) },
                new Author { Id = 5, Name = "Mikhail", Surname = "Lermontov", BirthDate = new DateOnly(1814, 10, 15) }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Captain's Daughter", YearOfPublication = 1836, Genre = "Historical novel" },
                new Book { Id = 2, Title = "War and Peace", YearOfPublication = 1867, Genre = "Historical novel" },
                new Book { Id = 3, Title = "Mumu", YearOfPublication = 1854, Genre = "Fiction " },
                new Book { Id = 4, Title = "Dead Souls", YearOfPublication = 1842, Genre = "Picaresque" },
                new Book { Id = 5, Title = "A Hero of Our Time", YearOfPublication = 1840, Genre = "Romance novel" }
                );
        }
    }
}
