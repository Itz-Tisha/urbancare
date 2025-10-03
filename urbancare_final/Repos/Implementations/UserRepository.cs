using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using urbancare_final.Models;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Repos.Implementations
{
    public class UserRepository : UserInterface
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void adduser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void make_complaints(Problem problem)
        {
            _context.Problems.Add(problem);
            _context.SaveChanges();
        }

        public ICollection<Resolution> GetResolutions()
        {
            return _context.Resolutions
                           .Include(r => r.Problem)
                           .Include(r => r.Department)
                           .Include(r => r.User)
                           .ToList();
        }

        public User getProfile(User user)
        {
            return _context.Users
                           .Include(u => u.Problems)
                           .Include(u => u.Resolutions)
                           .FirstOrDefault(u => u.Id == user.Id);
        }

        public User EditProfile(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                

                _context.Users.Update(existingUser);
                _context.SaveChanges();
            }
            return existingUser;
        }


        public ICollection<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users
                           .Include(u => u.Problems)
                           .Include(u => u.Resolutions)
                           .FirstOrDefault(u => u.Id == id);
        }
        public List<SelectListItem> GetDepartmentsForDropdown()
        {
            return _context.DepartmentMasters
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToList();
        }

        public List<Problem> GetComplaintsByUser(int userId)
        {
            return _context.Problems
                           .Include(p => p.DepartmentMaster)
                           .Include(p => p.Resolutions)
                           .Where(p => p.UserId == userId)
                           .OrderByDescending(p => p.DateSubmitted)
                           .ToList();
        }
        public List<Problem> GetAllComplaints()
        {
            return _context.Problems
                 .Include(p => p.DepartmentMaster)
                 .Include(p => p.Resolutions)
                 .ToList();
        }

        public List<Resolution> GetUserResolutions(int userId, int id)
        {
            return _context.Resolutions
        .Include(r => r.Problem)
        .Include(r => r.Department)
        .Include(r => r.User)
        .Where(r => r.UserId == userId && r.ProblemId == id)
        .ToList();
        }

        public bool DeleteProblem(int problemId, int userId)
        {
            var problem = _context.Problems
                                  .Include(p => p.Resolutions)
                                  .FirstOrDefault(p => p.Id == problemId && p.UserId == userId);

            if (problem == null)
                return false;

            if (problem.Resolutions != null && problem.Resolutions.Any())
            {
                _context.Resolutions.RemoveRange(problem.Resolutions);
            }

            _context.Problems.Remove(problem);
            _context.SaveChanges();
            return true;
        }

        public Problem GetComplaintById(int id)
        {
            return _context.Problems
                           .Include(p => p.DepartmentMaster)
                           .FirstOrDefault(p => p.Id == id);
        }

        public void UpdateComplaint(Problem complaint)
        {
            _context.Problems.Update(complaint);
            _context.SaveChanges();
        }

        public bool DepartmentExists(int departmentId)
        {
            return _context.DepartmentMasters.Any(d => d.Id == departmentId);
        }

        public Department GetDepartmentByPincode(int pincode, string city)
        {
            return _context.Departments
                .FirstOrDefault(d => d.ZipCode == pincode && d.City.ToLower() == city.ToLower());
        }


    }
}
