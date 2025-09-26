using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models
{
    public class DepartmentMaster
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; }
        public ICollection<Problem> Problems { get; set; }
    }
}
