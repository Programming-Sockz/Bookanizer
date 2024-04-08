using Bookanizer.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookanizer.Server.Services
{
    public class BookanizerDbContext : DbContext, IBookanizerDbContext
    {
        protected string Schema = "dbo";


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
