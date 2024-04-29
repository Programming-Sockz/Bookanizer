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


        public BookanizerDbContext(DbContextOptions<BookanizerDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
