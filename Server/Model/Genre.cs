namespace Bookanizer.Server.Model
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
