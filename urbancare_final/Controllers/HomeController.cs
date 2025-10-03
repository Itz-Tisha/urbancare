using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using urbancare_final.Models;
using urbancare_final.Repos.Interfaces;
using urbancare_final.ViewModels;

public class HomeController : Controller
{
    private readonly HomeInterface _homeRepository;
    private readonly AccountInterface _repo;
   
    public HomeController(HomeInterface homeRepository ,AccountInterface repo)
    {
        _homeRepository = homeRepository;
        _repo = repo;
      
    }

    [Authorize]
    public IActionResult Index()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var model = new HomeViewModel { Role = role };

        if (role == "Citizen")
        {
            model.User = _homeRepository.GetCitizenById(userId);
        }
        else if (role == "Department")
        {
            model.Department = _homeRepository.GetDepartmentById(userId);
        }

        return View(model);
    }

    public IActionResult ViewProfile()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var model = new HomeViewModel { Role = role };

        if (role == "Citizen")
        {
            model.User = _homeRepository.GetCitizenById(userId);
        }
        else if (role == "Department")
        {
            model.Department = _homeRepository.GetDepartmentById(userId);
        }

        return View(model);
    }


    //[Authorize]
    //public IActionResult Edit()
    //{
    //    var role = User.FindFirstValue(ClaimTypes.Role);
    //    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    //    var model = new HomeViewModel { Role = role };

    //    if (role == "Citizen")
    //    {
    //        model.User = _homeRepository.GetCitizenById(userId);
    //    }
    //    else if (role == "Department")
    //    {
    //        model.Department = _homeRepository.GetDepartmentById(userId);
    //    }

    //    return View(model);
    //}


    //[HttpPost]
    //[Authorize]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(HomeViewModel model, IFormFile PhotoFile)
    //{

    //    var role = User.FindFirstValue(ClaimTypes.Role);
    //    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));



    //    if (role == "Citizen" && model.User != null)
    //    {
    //        bool exist = _homeRepository.IsEmailExists(model.User.Email);
    //        if (exist)
    //        {
    //            // ViewBag.EmailError = "This email is already taken.";
    //            model.User = _homeRepository.GetCitizenById(userId);
    //            return View(model);

    //        }
    //        var user = _homeRepository.GetCitizenById(userId);
    //        if (user != null)
    //        {
    //            user.Name = model.User.Name;
    //            user.Email = model.User.Email;



    //            if (PhotoFile != null && PhotoFile.Length > 0)
    //            {
    //                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoFile.FileName)}";
    //                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
    //                Directory.CreateDirectory(uploadPath);
    //                var filePath = Path.Combine(uploadPath, fileName);

    //                using (var stream = new FileStream(filePath, FileMode.Create))
    //                {
    //                    await PhotoFile.CopyToAsync(stream);
    //                }

    //                user.Photo = $"/uploads/{fileName}";
    //            }

    //            _homeRepository.UpdateCitizen(user);
    //            await SignInAsync(user.Id.ToString(), user.Name, user.Email, "Citizen");
    //            await _homeRepository.SaveAsync();
    //        }
    //    }
    //    else if (role == "Department" && model.Department != null)
    //    {
    //        bool exist = _homeRepository.IsEmailExists(model.Department.Email);
    //        if (exist)
    //        {
    //             model.Department = _homeRepository.GetDepartmentById(userId);

    //            return View(model);

    //        }
    //        if(model.Department.ZipCode>999999 || model.Department.ZipCode <= 99999)
    //        {
    //            ViewBag.EmailError = "Invalid pincode.";
    //            model.Department = _homeRepository.GetDepartmentById(userId);
    //            return View(model);
    //        }
    //        var dept = _homeRepository.GetDepartmentById(userId);
    //        if (dept != null)
    //        {
    //            dept.user_Name = model.Department.user_Name;
    //            dept.Email = model.Department.Email;
    //            dept.City = model.Department.City;
    //            dept.ZipCode = model.Department.ZipCode;



    //            if (PhotoFile != null && PhotoFile.Length > 0)
    //            {
    //                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoFile.FileName)}";
    //                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
    //                Directory.CreateDirectory(uploadPath);
    //                var filePath = Path.Combine(uploadPath, fileName);

    //                using (var stream = new FileStream(filePath, FileMode.Create))
    //                {
    //                    await PhotoFile.CopyToAsync(stream);
    //                }

    //                dept.Photo = $"/uploads/{fileName}";
    //            }

    //            _homeRepository.UpdateDepartment(dept);
    //            await SignInAsync(dept.Id.ToString(), dept.Email, dept.Email, "Department");
    //            await _homeRepository.SaveAsync();
    //        }
    //    }

    //    return RedirectToAction("ViewProfile");
    //}
    [Authorize]
    public IActionResult Edit()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var model = new HomeViewModel { Role = role };

        if (role == "Citizen")
        {
            model.User = _homeRepository.GetCitizenById(userId);
        }
        else if (role == "Department")
        {
            model.Department = _homeRepository.GetDepartmentById(userId);
        }

        return View(model);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HomeViewModel model, IFormFile PhotoFile)
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (role == "Citizen" && model.User != null)
        {
            var user = _homeRepository.GetCitizenById(userId);
            if (user != null)
            {
                
                if (_homeRepository.CitizenEmailExists(model.User.Email, userId))
                {
                    ModelState.AddModelError("User.Email", "Email is already in use.");
                    model.Role = role;
                    return View(model);
                }

                user.Name = model.User.Name;
                user.Email = model.User.Email;

                if (PhotoFile != null && PhotoFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoFile.FileName)}";
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadPath);
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PhotoFile.CopyToAsync(stream);
                    }

                    user.Photo = $"/uploads/{fileName}";
                }

                _homeRepository.UpdateCitizen(user);
                await _homeRepository.SaveAsync();
            }
        }
        else if (role == "Department" && model.Department != null)
        {
            var dept = _homeRepository.GetDepartmentById(userId);
            if (dept != null)
            {
                
                if (_homeRepository.DepartmentEmailExists(model.Department.Email, userId))
                {
                    ModelState.AddModelError("Department.Email", "Email is already in use.");
                    model.Role = role;
                    return View(model);
                }

                if (model.Department.ZipCode < 100000 || model.Department.ZipCode > 999999)
                {
                    ModelState.AddModelError("Department.ZipCode", "Pincode must be exactly 6 digits.");
                    model.Role = role;
                    return View(model);
                }


                dept.user_Name = model.Department.user_Name;
                dept.Email = model.Department.Email;
                dept.City = model.Department.City;
                dept.ZipCode = model.Department.ZipCode;

                if (PhotoFile != null && PhotoFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(PhotoFile.FileName)}";
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadPath);
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PhotoFile.CopyToAsync(stream);
                    }

                    dept.Photo = $"/uploads/{fileName}";
                }

                _homeRepository.UpdateDepartment(dept);
                await _homeRepository.SaveAsync();
            }
        }

        return RedirectToAction("ViewProfile");
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

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });
    }






}
