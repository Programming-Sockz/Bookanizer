using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookanizer.Shared.DTO
{
    //das hier wird benutzt um sich zu registrieren. einfach damit das User model nie im frontend ist. 
    public class RegisterDTO
    {
        //Dieses [Required] hilft uns sicherzustellen das User das feld Ausfüllen müssen. Hier ist mal ein Link der es besser erklärt als ich
        //https://blazor-university.com/forms/validation/
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
