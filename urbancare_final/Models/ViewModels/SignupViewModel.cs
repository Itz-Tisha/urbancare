using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models
{
    public class SignupViewModel
    {
        public string Role { get; set; }

        public User User { get; set; } = new User();

        public Department Department { get; set; } = new Department();
    }
}
