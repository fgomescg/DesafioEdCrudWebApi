using Entities.Models;
using Entities.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext() { }
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookSubject> BookSubject { get; set; }
        public DbSet<BookAuthorReport> BookAuthorReport { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookAuthor>()
                .HasKey(BA => new { BA.BookId, BA.AuthorId });
            builder.Entity<BookSubject>()
                .HasKey(BS => new { BS.BookId, BS.SubjectId });

            builder.Entity<BookAuthorReport>().HasNoKey();
          
        }
    }
}
