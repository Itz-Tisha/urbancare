using System;
using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models
{
    public class Resolution
    {
        public int Id { get; set; }

        [Required]
        public string Response { get; set; }

        public DateTime Date { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProblemId { get; set; }

        public Problem Problem { get; set; }
    }
}
