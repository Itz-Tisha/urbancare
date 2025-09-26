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

        [ForeignKey("DepartmentMasterId")]
        public DepartmentMaster DepartmentMaster { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Problem> Problems { get; set; }

        public ICollection<Resolution> Resolutions { get; set; }
    }
}
