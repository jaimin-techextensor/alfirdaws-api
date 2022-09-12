﻿using System;
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
    [ApiController]
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
        /// Retrieves the overview of the roles including their permissions
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet]
        [Route("roles")]
        public IActionResult GetRolesOverview([FromQuery] PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                IActionResult? response = null;
                var result = _roleInterface.GetRolesOverview(pageParamsRequestModel);

                if (result != null)
                {
                    var metadata = new
                    {
                        result.TotalCount,
                        result.PageSize,
                        result.CurrentPage,
                        result.TotalPages,
                        result.HasNext,
                        result.HasPrevious
                    };
                    return response = Ok(new { Success = true, Message = "Retrieved roles overview", PageInfo = metadata, Data = result });
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
        /// Gets information of one role including its permissions
        /// </summary>
        /// <param name="id">Unique id of the role</param>
        /// <returns>Role object</returns>
        [HttpGet]
        [Route("roles/{id}")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _roleInterface.GetRoleById(id);
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
        /// Create new role including the permissions
        /// </summary>
        /// <param name="roleRequest">Role request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> CreateRole(RoleCreateRequest roleRequest)
        {
            try
            {
                IActionResult? response = null;
                if (roleRequest.Name == null || roleRequest.IsStatic == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill the required fields" });
                }
                var result = _roleInterface.CreateRole(roleRequest);
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
        /// Updates the information of a role including the permissions
        /// </summary>
        /// <param name="roleRequest">Role request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("roles")]
        public async Task<IActionResult> UpdateRole(RoleUpdateRequest roleRequest)
        {
            try
            {
                IActionResult? response = null;
                if (roleRequest.RoleId == null || roleRequest.Name == null || roleRequest.IsStatic == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _roleInterface.UpdateRole(roleRequest);
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

        [HttpGet]
        [Route("role-permission/{userId}")]
        public IActionResult GetRolePermissionByUser(int userId)
        {
            try
            {
                IActionResult? response = null;
                var result = _roleInterface.GetRolePermissionByUser(userId);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Role permission retrived successful !!!", Data = result });
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

