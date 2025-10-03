using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using urbancare_final.Models;
using urbancare_final.Models.ViewModels;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountInterface _repo;

        public AccountController(AccountInterface repo)
        {
            _repo = repo;
        }

        public IActionResult ChooseRole() => View();

        public IActionResult Login(string role)
        {
            var model = new AuthViewModel { Role = role };
            ViewBag.Role = role;
            return View(model);
        }

        public IActionResult Signup(string role)
        {
            var model = new AuthViewModel
            {
                Role = role,
                DepartmentOptions = _repo.GetDepartmentMasters()
            };
            ViewBag.Role = role;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DepartmentOptions = _repo.GetDepartmentMasters();
                ViewBag.Role = model.Role;
                return View(model);
            }

            bool emailExists = false;
            if (model.Role == "Citizen")
            {
                emailExists = _repo.CitizenExists(model.Email);
            }
            else if (model.Role == "Department")
            {
                emailExists = _repo.DepartmentExists(model.Email);
            }

            if (emailExists)
            {
              
                ModelState.AddModelError("Email", "This email is already registered.");
                model.DepartmentOptions = _repo.GetDepartmentMasters();
                ViewBag.Role = model.Role;
                return View(model);
            }

            if (model.Role == "Citizen")
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };
                _repo.AddCitizen(user);
            }
            else if (model.Role == "Department")
            {
                var dept = new Department
                {
                    user_Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    DepartmentMasterId = model.DepartmentMasterId,
                    City = model.City,
                    ZipCode = (int)model.ZipCode
                };
                _repo.AddDepartment(dept);
            }

            await _repo.SaveAsync();
            return RedirectToAction("Login", new { role = model.Role });
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DepartmentOptions = _repo.GetDepartmentMasters();
                ViewBag.Role = model.Role;
                return View(model);
            }

            if (model.Role == "Citizen")
            {
                var user = _repo.ValidateCitizen(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user.Id.ToString(), user.Name, user.Email, "Citizen");
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (model.Role == "Department")
            {
                var dept = _repo.ValidateDepartment(model.Email, model.Password);
                if (dept != null)
                {
                    await SignInAsync(dept.Id.ToString(), dept.Email, dept.Email, "Department");
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Role = model.Role;
            ModelState.AddModelError("", "Invalid login credentials");
            return View(model);
        }

        private async Task SignInAsync(string id, string name, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true });
        }

        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account"); 
        }
    }
}
