using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface IUserInterface
    {
        PagedList<UserModel> GetUsersOverview(PageParamsRequestModel pageParamsRequestModel);
        Task<UserModelResponse> GetUserById(int UserId);
        bool CreateUser(UserModel userModel);
        bool UpdateUser(UserModel userModel);
        bool DeleteUser(int UserId);
        bool ActivateDeactivateUser(int id, bool isActive);
        bool AssignRole(int userId, int roleId);
        bool RemoveRole(int userId, int roleId);
    }
}
