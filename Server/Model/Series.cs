using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Server.Model
{
    public class Series
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
