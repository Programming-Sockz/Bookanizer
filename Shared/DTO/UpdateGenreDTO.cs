namespace Bookanizer.Shared.DTO
{
    public class UpdateGenreDTO
    {
        public Guid BookId { get; set; }
        public List<Guid>? GenreIds { get; set; }
    }
}
