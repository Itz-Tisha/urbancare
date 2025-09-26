using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HomeViewModel model)
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
                _homeRepository.UpdateCitizen(user);
                await _homeRepository.SaveAsync();
            }
        }
        else if (role == "Department" && model.Department != null)
        {
            var dept = _homeRepository.GetDepartmentById(userId);
            if (dept != null)
            {
                dept.Email = model.Department.Email;
                dept.City = model.Department.City;
                dept.ZipCode = model.Department.ZipCode;
                _homeRepository.UpdateDepartment(dept);
                await _homeRepository.SaveAsync();
            }
        }

        return RedirectToAction("Index");
    }


}
