using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace alfirdawsmanager.Service.Service
{
    public class RoleService: IRoleInterface
    {

        #region Members

        private readonly AlfirdawsManagerDbContext _context;
        static IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        #endregion

        public RoleService(AlfirdawsManagerDbContext context, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Retrives all the roles within the platform
        /// </summary>
        /// <returns>List of Roles</returns>
        public async Task<List<Role>> GetRolesOverview()
        {
            try
            {
                var dataToReturn = new List<Role>();
                using (var repo = new RepositoryPattern<Role>())
                {
                    dataToReturn = _mapper.Map<List<Role>>(repo.SelectAll().OrderByDescending(a => a.RoleId).ToList());
                    return dataToReturn;
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
        public async Task<Role> GetRoleById(int RoleId)
        {
            try
            {
                var dataToReturn = new Role();
                using (var repo = new RepositoryPattern<Role>())
                {
                    dataToReturn = _mapper.Map<Role>(repo.SelectByID(RoleId));
                    
                }
                return dataToReturn;
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
        public async Task<List<Role>> SearchRoles(string searchText)
        {
            try
            {
                var dataToReturn = new List<Role>();
                if(searchText != null && searchText != String.Empty)
                {
                    using (var repo = new RepositoryPattern<Role>())
                    {
                        dataToReturn = _mapper.Map<List<Role>>(repo.SelectAll().OrderByDescending(a => a.RoleId)
                                                                    .Where(a =>
                                                                                ((a.Name != null) && (a.Name.Contains(searchText)))
                                                                             || ((a.Description != null) && (a.Description.Contains(searchText))                                                                        )                                                                      
                                                                   ).ToList());
                    }
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
        public bool CreateRole(RoleModel roleModel)
        {
            try
            {
                bool success = false;

                var objRole = new Role();
                objRole.Name = roleModel.Name;
                objRole.Description = roleModel.Description;
                objRole.IsStatic = roleModel.IsStatic;

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
        public bool UpdateRole(RoleModel roleModel)
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
        /// Deletes a specific role
        /// </summary>
        /// <param name="roleId">The unique id of the role that needs to be updated</param>
        /// <returns>Indication if the role was successfully deleted or not</returns>
        public bool DeleteRole(int roleId)
        {
            try
            {
                bool success = false;
                using (var repo = new RepositoryPattern<Role>())
                {
                    var objRole = _mapper.Map<Role>(repo.SelectByID(roleId));
                    if (objRole != null)
                    {
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

