using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    public class BookTagDTO
    {
        public Guid BookId { get; set; }
        public BookDTO Book { get; set; }

        public Guid TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}
