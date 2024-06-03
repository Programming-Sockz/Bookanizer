namespace Bookanizer.Server.Controller
{
    public class UpdateGenreDTO
    {
        public Guid BookId { get; set; }
        public List<Guid>? GenreIds { get; set; }
    }
}
