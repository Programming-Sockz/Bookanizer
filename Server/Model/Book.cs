using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Server.Model
{
    //Dieses Model ist unser C# gegenstück unserer SQL Datenbank.
    //Das heißt dieses Model muss genau gleich sein wie unsere SQL Tabelle
    public class Book
    {
        //das [Key] hier bezeichnet das die Id der Primary Key ist
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PageCount { get; set; }
        //ein ? nach dem Datentyp heißt das es null sein kann
        public Guid? AuthorId { get; set; }
        public Guid? SeriesId { get; set; }
    }
}
