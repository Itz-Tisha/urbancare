using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urbancare_final.Models;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Repos.Implementations
{
    public class AccountRepository : AccountInterface
    {

        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DepartmentMaster> GetDepartmentMasters()
        {
            return _context.DepartmentMasters.ToList();
        }

        public void AddCitizen(User user)
        {
            _context.Users.Add(user);
        }

        public User ValidateCitizen(string email, string password)
        {
            return _context.Users
                           .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void AddDepartment(Department dept)
        {
            _context.Departments.Add(dept);
        }

        public Department ValidateDepartment(string email, string password)
        {
            return _context.Departments
                           .Include(d => d.DepartmentMaster)
                           .FirstOrDefault(d => d.Email == email && d.Password == password);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CitizenExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool DepartmentExists(string email)
        {
            return _context.Departments.Any(d => d.Email == email);
        }

    }
}
