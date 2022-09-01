using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Interface
{
    public interface IUserInterface
    {
        PagedList<User> GetUsersOverview(PageParamsRequestModel pageParamsRequestModel);
        Task<List<User>> SearchUsers(string searchText);
        Task<UserModelResponse> GetUserById(int UserId);
        bool CreateUser(UserModel userModel);
        bool UpdateUser(UserModel userModel);
        bool DeleteUser(int UserId);
        bool ActivateDeactivateUser(int id, bool isActive);
        bool AssignRole(int userId, int roleId);
        bool RemoveRole(int userId, int roleId);
    }
}
