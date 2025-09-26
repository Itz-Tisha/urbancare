using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace urbancare_final.Models
{
    public class Problem
    {
        public int Id { get; set; }

        [Required]
        public string Statement { get; set; }

        [Required]
        public string ProblemType { get; set; }

        public string Photo { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DepartmentMasterId { get; set; }
        public DepartmentMaster DepartmentMaster { get; set; }

        public string city {  get; set; }
        public int pincode {  get; set; }

        public ICollection<Resolution> Resolutions { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

    }
}
