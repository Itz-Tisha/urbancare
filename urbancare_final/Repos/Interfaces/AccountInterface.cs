using System.Collections.Generic;
using System.Threading.Tasks;
using urbancare_final.Models;
namespace urbancare_final.Repos.Interfaces
{
    public interface AccountInterface
    {
        List<DepartmentMaster> GetDepartmentMasters();

        void AddCitizen(User user);
        User ValidateCitizen(string email, string password);

        void AddDepartment(Department dept);
        Department ValidateDepartment(string email, string password);

        Task SaveAsync();
        bool CitizenExists(string email);
        bool DepartmentExists(string email);
    }
}
