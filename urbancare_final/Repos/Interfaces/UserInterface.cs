using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using urbancare_final.Models;

namespace urbancare_final.Repos.Interfaces
{
    public interface UserInterface
    {
        void adduser(User user);

        void make_complaints(Problem problem);

        ICollection<Resolution> GetResolutions();

        User getProfile(User user);

        User EditProfile(User user);

        ICollection<User> GetAll();
        User GetById(int id);

        List<SelectListItem> GetDepartmentsForDropdown();

        List<Problem> GetComplaintsByUser(int userId);

        List<Problem> GetAllComplaints();

        List<Resolution> GetUserResolutions(int userId,int id);

        bool DeleteProblem(int problemId, int userId);

        Problem GetComplaintById(int id);
        void UpdateComplaint(Problem complaint);
        bool DepartmentExists(int departmentId);

    }
}
