using System.Threading.Tasks;
using urbancare_final.Models;

namespace urbancare_final.Repos.Interfaces
{
    public interface HomeInterface
    {
        User GetCitizenById(int id);
        Department GetDepartmentById(int id);

        void UpdateCitizen(User user);
        void UpdateDepartment(Department department);
        Task SaveAsync();
    }
}
