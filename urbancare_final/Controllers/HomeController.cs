using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using urbancare_final.Repos.Interfaces;
using urbancare_final.ViewModels;

public class HomeController : Controller
{
    private readonly HomeInterface _homeRepository;

    public HomeController(HomeInterface homeRepository)
    {
        _homeRepository = homeRepository;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Account");
    }

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




    //[HttpPost]
    //[Authorize]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(HomeViewModel model)
    //{
    //    var role = User.FindFirstValue(ClaimTypes.Role);
    //    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    //    string updatedDisplayName = null;
    //    if (role == "Citizen" && model.User != null)
    //    {
    //        var user = _homeRepository.GetCitizenById(userId);
    //        if (user != null)
    //        {
    //            user.Name = model.User.Name;
    //            user.Email = model.User.Email;
    //            user.Photo = model.User.Photo;
    //            _homeRepository.UpdateCitizen(user);
    //            await _homeRepository.SaveAsync();
    //            updatedDisplayName = user.Name;
    //        }
    //    }
    //    else if (role == "Department" && model.Department != null)
    //    {
    //        var dept = _homeRepository.GetDepartmentById(userId);
    //        if (dept != null)
    //        {
    //            // If your Department has a user display name field, set it too
    //            if (!string.IsNullOrWhiteSpace(model.Department.user_Name))
    //            {
    //                dept.user_Name = model.Department.user_Name;
    //                updatedDisplayName = dept.user_Name;
    //            }
    //            dept.Email = model.Department.Email;
    //            dept.City = model.Department.City;
    //            dept.ZipCode = model.Department.ZipCode;
    //            _homeRepository.UpdateDepartment(dept);
    //            await _homeRepository.SaveAsync();
    //        }
    //    }

    //    // Refresh authentication cookie so User.Identity.Name reflects changes immediately


    //    return RedirectToAction("Index");
    //}

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
                user.Name = model.User.Name;
                user.Email = model.User.Email;

                // Save photo if uploaded
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
                dept.user_Name = model.Department.user_Name;
                dept.Email = model.Department.Email;
                dept.City = model.Department.City;
                dept.ZipCode = model.Department.ZipCode;

                // Departments may also have a photo, optional:
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



}
