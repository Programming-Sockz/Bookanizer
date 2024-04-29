using System.ComponentModel.DataAnnotations;

namespace Bookanizer.Shared.DTO
{
    //Das hier wird nur genutzt um sich einzuloggen
    public class LoginDTO
    {
        //schaut ins registerDTO wegen den [Required]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
