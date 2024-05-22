using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    public class BookDTO
    {
        //nachdem das DTO nicht benutzt wird um auf unsere Datenbank zuzugreifen, wird hier kein [Key] benötigt
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PageCount { get; set; }
        //ein ? nach dem Datentyp heißt das es null sein kann
        public Guid? AuthorId { get; set; }
        public Guid? SeriesId { get; set; }


        public AuthorDTO? Author { get; set; }
    }
}
