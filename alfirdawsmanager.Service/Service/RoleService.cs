using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;

namespace alfirdawsmanager.Service.Service
{
    public class RoleService: IRoleInterface
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
        public Task<List<RoleModel>> GetRolesOverview()
        {
            try
            {
                var dataToReturn = new List<RoleModel>();

                var p_repo = new RepositoryPattern<Permission>();
                var m_repo = new RepositoryPattern<Module>();

                using (var repo = new RepositoryPattern<Role>())
                {
                    List<Role> roles = repo.SelectAll().OrderBy(a => a.RoleId).ToList();

                    foreach (var role in roles)
                    {
                        RoleModel roleModel = new RoleModel();
                        roleModel.RoleId = role.RoleId;
                        roleModel.Name = role.Name;
                        roleModel.Description = role.Description;
                        roleModel.IsStatic = role.IsStatic;
                        roleModel.Permissions = new List<PermissionsModel>();

                        using (p_repo)
                        {
                            List<Permission> permissions = p_repo.SelectAll().Where(p => p.RoleId == role.RoleId).OrderBy(p => p.PermissionId).ToList();

                            foreach(var perm in permissions)
                            {
                                PermissionsModel permModel = new PermissionsModel();
                                permModel.PermissionId = perm.PermissionId;
                                permModel.Create = perm.Create;
                                permModel.Read = perm.Read;
                                permModel.Update = perm.Update;
                                permModel.Delete = perm.Delete;

                                using ( m_repo)
                                {
                                    var module = m_repo.SelectAll().SingleOrDefault(m => m.ModuleId == perm.ModuleId);
                                    if(module != null)
                                    {
                                        permModel.ModuleId = module.ModuleId;
                                        permModel.ModuleName = module.Name;
                                    }
                                }

                                roleModel.Permissions.Add(permModel);
                            }
                        }
                              
                        dataToReturn.Add(roleModel);

                    }
                    return Task.FromResult(dataToReturn);
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
        public  Task<RoleModel> GetRoleById(int RoleId)
        {
            try
            {
                var dataToReturn = new RoleModel() ;

                var p_repo = new RepositoryPattern<Permission>();
                var m_repo = new RepositoryPattern<Module>();

                using (var repo = new RepositoryPattern<Role>())
                {
                    var role = repo.SelectByID(RoleId);
                    if(role != null)
                    {
                        RoleModel roleModel = new RoleModel();
                        roleModel.RoleId = role.RoleId;
                        roleModel.Name = role.Name;
                        roleModel.Description = role.Description;
                        roleModel.IsStatic = role.IsStatic;
                        roleModel.Permissions = new List<PermissionsModel>();

                        using (p_repo)
                        {
                            List<Permission> permissions = p_repo.SelectAll().Where(p => p.RoleId == role.RoleId).OrderBy(p => p.PermissionId).ToList();

                            foreach (var perm in permissions)
                            {
                                PermissionsModel permModel = new PermissionsModel();
                                permModel.PermissionId = perm.PermissionId;
                                permModel.Create = perm.Create;
                                permModel.Read = perm.Read;
                                permModel.Update = perm.Update;
                                permModel.Delete = perm.Delete;

                                using (m_repo)
                                {
                                    var module = m_repo.SelectAll().SingleOrDefault(m => m.ModuleId == perm.ModuleId);
                                    if (module != null)
                                    {
                                        permModel.ModuleId = module.ModuleId;
                                        permModel.ModuleName = module.Name;
                                    }
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
                }
                return Task.FromResult(dataToReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }

      
        /// <summary>
        /// Searches for a specific role based on the search text
        /// </summary>
        /// <param name="searchText">Text to be searched for</param>
        /// <returns>List of roles which match the search criteria</returns>
        public  Task<List<RoleModel>> SearchRoles(string searchText)
        {
            try
            {
                var dataToReturn = new List<RoleModel>();
                if(searchText != null && searchText != String.Empty)
                {
                    using (var repo = new RepositoryPattern<Role>())
                    {
                        dataToReturn = _mapper.Map<List<RoleModel>>(repo.SelectAll().OrderByDescending(a => a.RoleId)
                                                                    .Where(a =>
                                                                                ((a.Name != null) && (a.Name.Contains(searchText)))
                                                                             || ((a.Description != null) && (a.Description.Contains(searchText))                                                                        )                                                                      
                                                                   ).ToList());
                    }
                }
                return Task.FromResult(dataToReturn);
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

                if(roleModel.Permissions !=  null)
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

                    if(roleModel.Permissions != null)
                    {
                        objRole.Permissions = new List<Permission>();
                        foreach (var perm in roleModel.Permissions)
                        {
                            Permission permission = new Permission();
                            if(perm.PermissionId != null)
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

                    using (var repo = new RepositoryPattern<Role>())
                    {
                        repo.Update(objRole);
                        repo.Save();
                        success = true;
                    }
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
                            foreach(var perm in permissions)
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


    }
}

