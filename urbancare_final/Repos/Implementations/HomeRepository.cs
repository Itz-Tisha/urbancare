using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using urbancare_final.Models;
using urbancare_final.Repos.Interfaces;

namespace urbancare_final.Repos.Implementations
{
    public class HomeRepository : HomeInterface
    {

        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetCitizenById(int id)
        {
            return _context.Users
                           .Include(u => u.Problems)
                           .Include(u => u.Resolutions)
                           .FirstOrDefault(u => u.Id == id);
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments
                           .Include(d => d.DepartmentMaster)
                           .Include(d => d.Problems)
                           .Include(d => d.Resolutions)
                           .FirstOrDefault(d => d.Id == id);
        }

        public void UpdateCitizen(User user)
        {
            _context.Users.Update(user);
        }

        public void UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool IsEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email )
                   || _context.Departments.Any(d => d.Email == email);
        }


        public bool CitizenEmailExists(string email, int excludeId)
        {
            return _context.Users.Any(u => u.Email == email && u.Id != excludeId);
        }

        public bool DepartmentEmailExists(string email, int excludeId)
        {
            return _context.Departments.Any(d => d.Email == email && d.Id != excludeId);
        }


    }
}
