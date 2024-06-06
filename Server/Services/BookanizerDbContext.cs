using Bookanizer.Server.Interfaces;
using Bookanizer.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Bookanizer.Server.Services
{
    public class BookanizerDbContext : DbContext, IBookanizerDbContext
    {
        protected string Schema = "dbo";

        public DbSet<Test> Test { get; set; }

        //Sachen die beachtet werden müssen:
        //Das Model muss in diese klammer rein <>
        //Der Name muss der gleiche name der SQL Tabelle sein
        //dieses {get;set;} sieht man immer mal wieder und können für dieses program ignoriert werden, die benutzt man soweit ich weiß nur um methoden auszuführen wenn man die variable aufruft oder abändernt.
        //Wenn man hier ein DbSet einträgt auch im Interface IBookanizerDbContext angeben
        public DbSet<Book> Books { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Tag> Tag { get; set; }
        //public DbSet<Series> Series { get; set; }
        public DbSet<BookList> BookList { get; set; }

        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookTags> BookTags { get; set; }
        public DbSet<BookBookList> BookBookList { get; set; }


        public BookanizerDbContext(DbContextOptions<BookanizerDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //das hier erstellt für unser Programm die relation unserer SQL zusammen
            //im Book.sql haben wir den foreign key erstellt und hier bauen wir ihn auch auf.
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)                // Each Book has one Author
                .WithMany()                           // An Author can have many Books
                .HasForeignKey(b => b.AuthorId);     // Foreign key is AuthorId in Book

            //modelBuilder.Entity<Book>()
            //    .HasOne(b => b.Series)
            //    .WithMany()
            //    .HasForeignKey(b => b.SeriesId);


            modelBuilder.Entity<BookGenre>(entity =>
            {
                entity.ToTable("BookGenre");
                entity.HasKey(e => new { e.BookId, e.GenreId });

                entity.HasOne(bg => bg.Book)
                    .WithMany(b => b.BookGenres)
                    .HasForeignKey(bg => bg.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bg => bg.Genre)
                    .WithMany(g => g.BookGenres)
                    .HasForeignKey(bg => bg.GenreId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BookTags>(entity =>
            {
                entity.ToTable("BookTags");
                entity.HasKey(e => new { e.BookId, e.TagId });

                entity.HasOne(bg => bg.Book)
                    .WithMany(b => b.BookTags)
                    .HasForeignKey(bg => bg.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bg => bg.Tag)
                    .WithMany(g => g.BookTags)
                    .HasForeignKey(bg => bg.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BookBookList>().HasKey(bbl => new
            {
                bbl.BookId,
                bbl.BookListId
            });

            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
