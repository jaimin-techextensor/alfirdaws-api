using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Interface
{
    public interface IUserInterface
    {
        Task<List<User>> GetUsersOverview();
        Task<List<User>> SearchUsers(string searchText);
        Task<User> GetUserById(int UserId);
        bool CreateUser(UserModel userModel);
        bool UpdateUser(UserModel userModel);
        bool DeleteUser(int UserId);
    }
}
