using System.ComponentModel.DataAnnotations;
using Bookanizer.Shared.Enums;
namespace Bookanizer.Server.Model
{
    public class BookList
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }
        public ListTypes ListType { get; set; }
        
    }
}
