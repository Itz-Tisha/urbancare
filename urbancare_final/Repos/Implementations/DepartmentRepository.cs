
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using urbancare_final.Models;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Repos.Implementations
{
    public class DepartmentRepository : DepartmentInterface
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void add_dept(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public ICollection<Problem> GetProblemsByDepartmentMaster(int deptmasterId, string city, int zipcode)
        {
            return _context.Problems
                  .Include(p => p.User)
                  .Include(p => p.Resolutions)
                  .Include(p => p.DepartmentMaster)
                  .Where(p => p.DepartmentMasterId == deptmasterId)
                  .Where(p => p.city == city).
                  Where(p => p.pincode == zipcode)
                  .ToList();
        }

       public  Department DepartmentuserByEmail(string email)
        {
            return _context.Departments.FirstOrDefault(d => d.Email == email);

        }


        public Problem GetProblemById(int id)
        {
            return _context.Problems.Include(p => p.User).FirstOrDefault(p => p.Id == id);
        }

        public void SaveResolution(Resolution resolution)
        {
            _context.Resolutions.Add(resolution);
            _context.SaveChanges();
        }


        public ICollection<Problem> Delete_problem(Problem problem)
        {
            var existingProblem = _context.Problems.Find(problem.Id);
            if (existingProblem != null)
            {
                _context.Problems.Remove(existingProblem);
                _context.SaveChanges();
            }

            return _context.Problems
                           .Include(p => p.User)
                           .Include(p => p.Resolutions)
                           .Include(p => p.DepartmentMaster)
                           .ToList();
        }

       
        public ICollection<Resolution> seeResolution(Department department)
        {
            return _context.Resolutions
                           .Include(r => r.Problem)
                           .Include(r => r.User)
                           .Where(r => r.DepartmentId == department.Id)
                           .ToList();
        }

        public void make_resolution(Resolution resolution)
        {
            _context.Resolutions.Add(resolution);
            _context.SaveChanges();
        }

        public ICollection<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments
                           .Include(d => d.Problems)
                           .Include(d => d.Resolutions)
                           .FirstOrDefault(d => d.Id == id);
        }
    }
}
