using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    //das ist das DTO was der User zurückbekommt sobald man sich registriert oder eingeloggt hat.
    public class LoginResponseDTO
    {
        //Ich hab keine ahnung wie ich es noch mache aber ich werde das warscheinlich irgendwie nutzen um zu checken ob der User eingeloggt ist.
        public Guid? LoginStamp { get; set; }
        public Guid? UserId { get; set; }
        public bool Success { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
