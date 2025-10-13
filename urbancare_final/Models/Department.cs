using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urbancare_final.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string user_Name { get; set; }

        [Required]
        public int DepartmentMasterId { get; set; }

       
        public DepartmentMaster DepartmentMaster { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }


        [Required]
        [Range(100000, 999999, ErrorMessage = "Pincode must be exactly 6 digits.")]
        public int ZipCode { get; set; }


        [Required]
        public string City { get; set; }

      
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6,
           ErrorMessage = "Password must be between 6 and 20 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).+$",
           ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit, and one special character.")]
        public string Password { get; set; }

        public ICollection<Problem> Problems { get; set; }

        public ICollection<Resolution> Resolutions { get; set; }
        public string Photo { get;  set; }
    }
}
