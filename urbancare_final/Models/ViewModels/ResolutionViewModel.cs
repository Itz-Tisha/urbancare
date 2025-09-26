using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models.ViewModels
{
    public class ResolutionViewModel
    {
        public int ProblemId { get; set; }

        [Required]
        public string Response { get; set; }
    }

}
