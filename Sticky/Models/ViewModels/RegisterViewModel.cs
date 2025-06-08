using System.ComponentModel.DataAnnotations;

namespace Sticky.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression("^(?!\\d+$).+", ErrorMessage = "Username cannot be numbers only.")]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage = "Password must contain only lowercase letters and numbers.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
