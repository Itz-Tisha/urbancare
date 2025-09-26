using System.Collections.Generic;
using urbancare_final.Models;

namespace urbancare_final.Repos.Interfaces
{
    public interface DepartmentInterface
    {
        void add_dept(Department department);

        ICollection<Problem> GetProblemsByDepartmentMaster(int deptmasterId,string city,int zipcode);

        ICollection<Problem> Delete_problem(Problem problem);

        ICollection<Resolution> seeResolution(Department department);

        void make_resolution(Resolution resolution);

        ICollection<Department> GetAll();
        Department GetById(int id);

        Department DepartmentuserByEmail(string email);

        Problem GetProblemById(int id);
        void SaveResolution(Resolution resolution);

    }
}
