using Bookanizer.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace Bookanizer.Server.Interfaces
{
    public interface IBookanizerDbContext
    {
        public DbSet<Test> Test { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
