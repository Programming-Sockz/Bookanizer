using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Server.Model
{
    public class Test
    {
        [Key] public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YIppe {  get; set; }
    }
}
