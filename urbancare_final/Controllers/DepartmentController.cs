using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using urbancare_final.Models;
using urbancare_final.Models.ViewModels;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentInterface _deptRepo;


        public DepartmentController(DepartmentInterface deptRepo)
        {
            _deptRepo = deptRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewComplaint()
        {
            
            string email = User.FindFirst(ClaimTypes.Email)?.Value; 

           
            var department = _deptRepo.DepartmentuserByEmail(email);            

            if (department == null)
            {
                return Unauthorized();
            }

            var complaints = _deptRepo.GetProblemsByDepartmentMaster(department.DepartmentMasterId,department.City,department.ZipCode);

            return View(complaints);
        }


        public IActionResult makeresolution(int problemId)
        {
            var model = new ResolutionViewModel
            {
                ProblemId = problemId
            };

            return View(model);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult makeresolution(ResolutionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
           
            var department = _deptRepo.DepartmentuserByEmail(email);

            if (department == null)
            {
                return Unauthorized();
            }

            var problem = _deptRepo.GetProblemById(model.ProblemId);
            if (problem == null)
            {
                return NotFound("Problem not found.");
            }

            var resolution = new Resolution
            {
                Response = model.Response,
                Date = DateTime.Now,
                DepartmentId = department.Id,
                UserId = problem.UserId,
                ProblemId = problem.Id
            };

            _deptRepo.SaveResolution(resolution);

            return RedirectToAction("ViewComplaint");
        }

    }
}
