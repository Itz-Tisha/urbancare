using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Problem> Problems { get; set; }

        public ICollection<Resolution> Resolutions { get; set; }
    }
}
