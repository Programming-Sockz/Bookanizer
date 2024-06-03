using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bookanizer.Shared.Libraries.ApiRoutes;

namespace Bookanizer.Shared.DTO
{
    public class BookGenreDTO
    {
        public Guid BookId { get; set; }
        public BookDTO Book { get; set; }

        public Guid GenreId { get; set; }
        public GenreDTO Genre { get; set; }
    }
}
