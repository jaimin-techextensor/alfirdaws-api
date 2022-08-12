using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Service.Interface
{
    public interface IRoleInterface
    {
        Task<List<Role>> GetRolesOverview();
        Task<List<Role>> SearchRoles(string searchText);
        Task<Role> GetRoleById(int RoleId);
        bool CreateRole(RoleModel roleModel);
        bool UpdateRole(RoleModel roleModel);
        bool DeleteRole(int roleId);
    }
}

