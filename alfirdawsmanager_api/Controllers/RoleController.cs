using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [Route("api")]
    public class RoleController : Controller
    {

        #region Members

        private IRoleInterface _roleInterface;

        #endregion

        #region Constructors

        public RoleController(IRoleInterface roleInterface)
        {
            _roleInterface = roleInterface ?? throw new ArgumentNullException(nameof(roleInterface));
        }

        #endregion


        /// <summary>
        /// Retrieves the overview of the roles
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRolesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _roleInterface.GetRolesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved roles overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve roles overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       


        /// <summary>
        /// Searches for a specific role
        /// </summary>
        /// <param name="searchRequest">The search text</param>
        /// <returns>List of roles</returns>
        [HttpGet]
        [Route("roles/search")]
        public async Task<IActionResult> SearchRoles([FromQuery] SearchRequest searchRequest)
        {
            try
            {
                IActionResult? response = null;
                var result = await _roleInterface.SearchRoles(searchRequest.SearchText);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Searched roles retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve searched roles" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one role
        /// </summary>
        /// <param name="id">Unique id of the role</param>
        /// <returns>Role object</returns>
        [HttpGet]
        [Route("roles/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _roleInterface.GetRoleById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get role by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve role by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="roleModel">Role object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> CreateRole([FromForm] RoleModel roleModel)
        {
            try
            {
                IActionResult? response = null;
                if (roleModel.Name == null || roleModel.IsStatic == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill the required fields" });
                }
                var result =  _roleInterface.CreateRole(roleModel);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Role created successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the information of a role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("roles")]
        public async Task<IActionResult> UpdateRole([FromForm] RoleModel roleModel)
        {
            try
            {
                IActionResult? response = null;
                if (roleModel.Name == null || roleModel.IsStatic == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill the required fields" });
                }
                var result = _roleInterface.UpdateRole(roleModel);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Role updated successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="id">Unique id of the role that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("roles/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _roleInterface.DeleteRole(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Role deleted successfully !!!" });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

