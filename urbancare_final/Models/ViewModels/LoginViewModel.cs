using System.ComponentModel.DataAnnotations;
namespace urbancare_final.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(20, MinimumLength = 6,
        //   ErrorMessage = "Password must be between 6 and 20 characters.")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).+$",
        //   ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit, and one special character.")]
        public string Password { get; set; }
    }
}
