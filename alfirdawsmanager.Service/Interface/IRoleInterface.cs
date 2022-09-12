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
    public interface IRoleInterface
    {
       PagedList<Role> GetRolesOverview(PageParamsRequestModel pageParamsRequestModel);
        RoleModel GetRoleById(int RoleId);
        bool CreateRole(RoleCreateRequest roleModel);
        bool UpdateRole(RoleUpdateRequest roleModel);
        bool DeleteRole(int roleId);
        List<RoleModel> GetRolePermissionByUser(int userId);
    }
}

