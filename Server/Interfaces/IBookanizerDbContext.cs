using Bookanizer.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Bookanizer.Server.Interfaces
{
    //ein Interface zeigt nur an welche Funktionen und Variablen es gibt im tatsächlichen Service. Sie werden hier aber nicht beschrieben!
    //Sie werden gleich genannt wie der Service aber mit einem I am anfang
    public interface IBookanizerDbContext
    {
        public DbSet<Test> Test { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
