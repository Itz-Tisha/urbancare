using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using urbancare_final.Models;
using urbancare_final.Models.ViewModels;
using urbancare_final.Repos.Implementations;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInterface _userRepo;

        public UserController(UserInterface userRepo)
        {
            _userRepo = userRepo;
        }



        public IActionResult Resolutions()
        {
            var resolutions = _userRepo.GetResolutions();
            return View(resolutions);
        }



        private string SaveFile(IFormFile photoFile)
        {
            if (photoFile == null || photoFile.Length == 0)
                return null;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + photoFile.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photoFile.CopyTo(fileStream);
            }

            return "uploads/" + uniqueFileName;
        }




        public IActionResult ViewAllComplaintsByUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);
            var complaints = _userRepo.GetComplaintsByUser(userId);
            return View(complaints);
        }

        public IActionResult ViewAllComplaints()
        {
            var complaints = _userRepo.GetAllComplaints();
            return View(complaints);
        }

        public IActionResult ViewResolution(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var resolutions = _userRepo.GetUserResolutions(userId, id);

            if (resolutions == null || !resolutions.Any())
            {
                ViewBag.Message = "No resolution found.";
                return View(new List<Resolution>());
            }

            return View(resolutions);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete_Problem(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            bool deleted = _userRepo.DeleteProblem(id, userId);

            if (!deleted)
            {
                TempData["Error"] = "Problem not found or you are not authorized to delete.";
            }
            else
            {
                TempData["Success"] = "Problem deleted successfully.";
            }

            return RedirectToAction("ViewAllComplaintsByUser");
        }




        [Authorize(Roles = "Citizen")]
        [HttpGet]
        public IActionResult EditComplaint(int id)
        {
            var complaint = _userRepo.GetComplaintById(id);
            if (complaint == null) return NotFound();

            ViewBag.Departments = _userRepo.GetDepartmentsForDropdown();
            return View(complaint);
        }

      


        [Authorize(Roles = "Citizen")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditComplaint(Problem model, IFormFile PhotoFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _userRepo.GetDepartmentsForDropdown();
                return View(model);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentUserId))
            {
                return Unauthorized();
            }

            var complaint = _userRepo.GetComplaintById(model.Id);

            if (complaint == null || complaint.UserId != currentUserId)
            {
                return Unauthorized();
            }

          
            if (!_userRepo.DepartmentExists(model.DepartmentMasterId))
            {
                ModelState.AddModelError("DepartmentMasterId", "Selected department does not exist.");
                ViewBag.Departments = _userRepo.GetDepartmentsForDropdown();
                return View(model);
            }

            complaint.Statement = model.Statement;
            complaint.ProblemType = model.ProblemType;
            complaint.DepartmentMasterId = model.DepartmentMasterId;
            complaint.city = model.city;
            complaint.pincode = model.pincode;
           

            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                complaint.Photo = SaveFile(PhotoFile);
            }

            _userRepo.UpdateComplaint(complaint);

            return RedirectToAction("ViewAllComplaintsByUser");
        }



        [HttpGet]
        public IActionResult MakeComplaint()
        {
            var model = new MakeComplaintViewModel
            {
                DepartmentList = _userRepo.GetDepartmentsForDropdown()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeComplaint(MakeComplaintViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DepartmentList = _userRepo.GetDepartmentsForDropdown();
                return View(model);
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentUserId))
            {
                return Unauthorized();
            }

            var department = _userRepo.GetDepartmentByPincode(model.pincode,model.city);

            if (department == null )
            {
               
                return RedirectToAction("DepartmentNotAvailable", new { pincode = model.pincode });
            }

         
            var problem = new Problem
            {
                Statement = model.Statement,
                ProblemType = model.ProblemType,
                DepartmentMasterId = model.DepartmentId, 
                UserId = currentUserId,
                DateSubmitted = DateTime.Now,
                city = model.city,
                pincode = model.pincode
            };

            if (model.PhotoFile != null && model.PhotoFile.Length > 0)
            {
                problem.Photo = SaveFile(model.PhotoFile);
            }

            _userRepo.make_complaints(problem);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DepartmentNotAvailable(int pincode)
        {
            ViewBag.Pincode = pincode;
            return View();
        }



    }
}
