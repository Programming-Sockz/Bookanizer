using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    public class UpdateTagDTO
    {
        public Guid BookId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
