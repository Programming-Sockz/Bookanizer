using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    //Das hier ist ein abgespecktes User Model was andere User sehen können.
    //Es beinhaltet deswegen Natürlich keine Passwörter und andere sensitive Daten
    //Für Registrierung und Login bitte die dafür erstellten DTOs benutzen.
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
