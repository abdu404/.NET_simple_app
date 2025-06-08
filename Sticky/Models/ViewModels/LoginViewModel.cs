using System.ComponentModel.DataAnnotations;

namespace Sticky.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression("^(?!\\d+$).+", ErrorMessage = "Username cannot be numbers only.")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage = "Password must contain only lowercase letters and numbers.")]
        public string Password { get; set; }
    }
}
