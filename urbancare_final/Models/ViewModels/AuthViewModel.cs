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

        [Required]
        public string Password { get; set; }

        public int DepartmentMasterId { get; set; }

        public List<DepartmentMaster> DepartmentOptions { get; set; }

        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
