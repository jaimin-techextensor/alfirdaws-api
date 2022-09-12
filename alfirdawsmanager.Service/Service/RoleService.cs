using System;
using System.Data;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Service.Service
{
    public class RoleService : IRoleInterface
    {

        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        #endregion

        public RoleService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrives all the roles with their associated permissions within the platform
        /// </summary>
        /// <returns>List of Roles</returns>
        public PagedList<Role> GetRolesOverview(PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                var dataToReturn = new List<RoleModel>();

                var p_repo = new RepositoryPattern<Permission>();
                var m_repo = new RepositoryPattern<Module>();

                using (var repo = new RepositoryPattern<Role>())
                {
                    List<Role> roles = new List<Role>();
                    if (!string.IsNullOrEmpty(pageParamsRequestModel.SearchText) && pageParamsRequestModel.SearchText != "null")
                    {
                        return PagedList<Role>.ToPagedList(repo.SelectAll().OrderByDescending(a => a.RoleId)
                                                                    .Where(a =>
                                                                                ((a.Name != null) && (a.Name.Contains(pageParamsRequestModel.SearchText, StringComparison.OrdinalIgnoreCase)))
                                                                             || ((a.Description != null) && (a.Description.Contains(pageParamsRequestModel.SearchText, StringComparison.OrdinalIgnoreCase)))
                                                                   ).AsQueryable(), pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);
                    }
                    else
                    {
                        return PagedList<Role>.ToPagedList(repo.SelectAll().OrderBy(a => a.RoleId).AsQueryable(), pageParamsRequestModel.PageNumber, pageParamsRequestModel.PageSize);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Returns a specific role based on the given RoleId
        /// </summary>
        /// <param name="RoleId">The unique id of the role</param>
        /// <returns>A Role object</returns>
        public RoleModel GetRoleById(int RoleId)
        {
            try
            {
                var dataToReturn = new RoleModel();

                var role = _context.Roles.Where(a => a.RoleId == RoleId).Include(a => a.Permissions).FirstOrDefault();
                if (role != null)
                {
                    RoleModel roleModel = new RoleModel();
                    roleModel.RoleId = role.RoleId;
                    roleModel.Name = role.Name;
                    roleModel.Description = role.Description;
                    roleModel.IsStatic = role.IsStatic;
                    roleModel.Permissions = new List<PermissionsModel>();

                    if (role.Permissions != null && role.Permissions.Count > 0)
                    {
                        List<Permission> permissions = role.Permissions.OrderBy(p => p.PermissionId).ToList();

                        foreach (var perm in permissions)
                        {
                            PermissionsModel permModel = new PermissionsModel();
                            permModel.PermissionId = perm.PermissionId;
                            permModel.Create = perm.Create;
                            permModel.Read = perm.Read;
                            permModel.Update = perm.Update;
                            permModel.Delete = perm.Delete;

                            var module = _context.Modules.SingleOrDefault(m => m.ModuleId == perm.ModuleId);
                            if (module != null)
                            {
                                permModel.ModuleId = module.ModuleId;
                                permModel.ModuleName = module.Name;
                            }

                            roleModel.Permissions.Add(permModel);
                        }
                    }
                    dataToReturn = roleModel;
                }
                else
                {
                    dataToReturn = null;
                }
                return dataToReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="roleModel">a Role object</param>
        /// <returns>Indication if the creation was succesfull or not</returns>
        public bool CreateRole(RoleCreateRequest roleModel)
        {
            try
            {
                bool success = false;

                var objRole = new Role();
                objRole.Name = roleModel.Name;
                objRole.Description = roleModel.Description;
                objRole.IsStatic = roleModel.IsStatic;

                if (roleModel.Permissions != null && roleModel.Permissions.Count > 0)
                {
                    objRole.Permissions = new List<Permission>();
                    foreach (var perm in roleModel.Permissions)
                    {
                        Permission permission = new Permission();
                        permission.ModuleId = perm.ModuleId;
                        permission.Create = perm.Create;
                        permission.Read = perm.Read;
                        permission.Update = perm.Update;
                        permission.Delete = perm.Delete;

                        objRole.Permissions.Add(permission);
                    }
                }

                using (var repo = new RepositoryPattern<Role>())
                {
                    repo.Insert(objRole);
                    repo.Save();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates the information of a role
        /// </summary>
        /// <param name="roleModel">The role object that will be updated</param>
        /// <returns>Indication if the update was succesfull or not</returns>
        public bool UpdateRole(RoleUpdateRequest roleModel)
        {
            try
            {
                bool success = false;

                var objRole = _context.Roles.Where(a => a.RoleId == roleModel.RoleId).SingleOrDefault();
                if (objRole != null)
                {
                    objRole.Name = roleModel.Name;
                    objRole.Description = roleModel.Description;
                    objRole.IsStatic = roleModel.IsStatic;

                    if (roleModel.Permissions != null && roleModel.Permissions.Count > 0)
                    {
                        objRole.Permissions = new List<Permission>();
                        foreach (var perm in roleModel.Permissions)
                        {
                            Permission permission = new Permission();
                            if (perm.PermissionId != null)
                            {
                                permission.PermissionId = (int)perm.PermissionId;
                            }
                            permission.RoleId = objRole.RoleId;
                            permission.ModuleId = perm.ModuleId;
                            permission.Create = perm.Create;
                            permission.Read = perm.Read;
                            permission.Update = perm.Update;
                            permission.Delete = perm.Delete;

                            objRole.Permissions.Add(permission);
                        }
                    }

                    _context.SaveChanges();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a specific role with all its permissions
        /// </summary>
        /// <param name="roleId">The unique id of the role that needs to be updated</param>
        /// <returns>Indication if the role was successfully deleted or not</returns>
        public bool DeleteRole(int roleId)
        {
            try
            {
                bool success = false;
                var p_repo = new RepositoryPattern<Permission>();

                using (var repo = new RepositoryPattern<Role>())
                {
                    var objRole = _mapper.Map<Role>(repo.SelectByID(roleId));

                    if (objRole != null)
                    {
                        var permissions = p_repo.SelectAll().Where(p => p.RoleId == roleId).ToList();
                        if (permissions != null)
                        {
                            foreach (var perm in permissions)
                            {
                                p_repo.Delete(perm.PermissionId);
                                p_repo.Save();
                            }
                        }

                        repo.Delete(roleId);
                        repo.Save();
                        success = true;
                    }
                    return success;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get roles and permissions by user
        /// </summary>
        /// <param name="userId">UserId for which roles and permissions needs to be fetched</param>
        /// <returns>return result if any found</returns>
        public List<RoleModel> GetRolePermissionByUser(int userId)
        {
            try
            {
                var dataToReturn = new List<RoleModel>();
                var assignedRoles = _context.AssignedRoles.Where(a => a.UserId == userId).Include(a => a.Role);
                var assigned_Roles = assignedRoles.ToList();

                foreach (var assignedRole in assigned_Roles)
                {
                    if (assignedRole.Role != null)
                    {
                        RoleModel roleModel = new RoleModel();
                        roleModel.RoleId = assignedRole.RoleId;
                        roleModel.Name = assignedRole.Role.Name;
                        roleModel.Description = assignedRole.Role.Description;
                        roleModel.IsStatic = assignedRole.Role.IsStatic;
                        roleModel.Permissions = new List<PermissionsModel>();

                        var permissions = _context.Roles.Where(p => p.RoleId == roleModel.RoleId).Include(b => b.Permissions);
                        var permissionList = permissions.ToList();

                        foreach (var permission in permissionList[0].Permissions)
                        {
                            PermissionsModel permModel = new PermissionsModel();
                            permModel.PermissionId = permission.PermissionId;
                            permModel.Create = permission.Create;
                            permModel.Read = permission.Read;
                            permModel.Update = permission.Update;
                            permModel.Delete = permission.Delete;

                            var module = _context.Modules.SingleOrDefault(m => m.ModuleId == permission.ModuleId);
                            if (module != null)
                            {
                                permModel.ModuleId = module.ModuleId;
                                permModel.ModuleName = module.Name;
                            }

                            roleModel.Permissions.Add(permModel);
                        }
                        dataToReturn.Add(roleModel);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

