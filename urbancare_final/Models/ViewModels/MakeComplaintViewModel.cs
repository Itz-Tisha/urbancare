using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace urbancare_final.Models.ViewModels
{
    public class MakeComplaintViewModel
    {
        [Required]
        public string Statement { get; set; }

        [Required]
        public string ProblemType { get; set; }

        public IFormFile PhotoFile { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string city { get; set; }
    


        [Required]
        [Range(100000, 999999, ErrorMessage = "Pincode must be exactly 6 digits.")]
        public int pincode { get; set; }


        public List<SelectListItem> DepartmentList { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

    }
}
