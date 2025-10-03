using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using urbancare_final.Models;

namespace urbancare_final.Models.ViewModels
{
    public class AuthViewModel
    {
        [Required]
        public string Role { get; set; }

        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

      
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6,
          ErrorMessage = "Password must be between 6 and 20 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).+$",
          ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit, and one special character.")]
        public string Password { get; set; }

        public int DepartmentMasterId { get; set; }

        public List<DepartmentMaster> DepartmentOptions { get; set; }

        public string City { get; set; }
     

        [Range(100000, 999999, ErrorMessage = "Pincode must be exactly 6 digits.")]
        public int? ZipCode { get; set; }   


    }
}
