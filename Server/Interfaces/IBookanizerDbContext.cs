namespace Bookanizer.Server.Interfaces
{
    public interface IBookanizerDbContext
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
