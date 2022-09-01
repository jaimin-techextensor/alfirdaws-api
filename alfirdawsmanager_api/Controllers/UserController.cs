using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Members

        private IUserInterface _userInterface;

        #endregion

        #region Constructors

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get overview of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public IActionResult GetUsersOverview([FromQuery] PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                IActionResult response = null;
                var result = _userInterface.GetUsersOverview(pageParamsRequestModel);
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
                    return response = Ok(new { Success = true, Message = "Retrieved users overview", PageInfo = metadata, Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve users overview" });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="searchUsersRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/search")]
        public async Task<IActionResult> SearchUsers([FromQuery] SearchUsersRequest searchUsersRequest)
        {
            try
            {
                IActionResult response = null;
                var result = await _userInterface.SearchUsers(searchUsersRequest.SearchText);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Searched users retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve searched users" });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get information of one user
        /// </summary>
        /// <param name="UserId">Unique id of the user</param>
        /// <param name="isAvatar">Indication if the avatar information is requested</param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUserById(int id, [FromQuery] bool isAvatar = false)
        {
            try
            {
                IActionResult response = null;
                var result = await _userInterface.GetUserById(id);
                if (result != null)
                {
                    if(isAvatar)
                    {
                        var User = new
                        {
                            userId = result.UserId,
                            name = result.Name + ' ' + result.LastName,
                            email = result.Email,
                            picture = result.Picture,
                            status = result.Active==true? "online": "not-visible",

                        };

                        return response = Ok(new {User});
                    }
                    else
                    {
                        return response = Ok(new { Success = true, Message = "Get user by Id retrieved", Data = result });
                    }
                   
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve user by Id" });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> CreateUser(UserModel userModel)
        {
            try
            {
                IActionResult response = null;
                if (userModel.UserName == null || userModel.Name == null || userModel.LastName == null || userModel.Email == null || userModel.Password == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill the required fields" });
                }
                var result = _userInterface.CreateUser(userModel);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User created successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Update information of one user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("users")]
        public async Task<IActionResult> UpdateUser(UserModel userModel)
        {
            try
            {
                IActionResult response = null;
                if (userModel.UserName == null || userModel.Name == null || userModel.LastName == null || userModel.Email == null || userModel.Password == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill the required fields" });
                }
                var result = _userInterface.UpdateUser(userModel);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User updated successfully !!!" });
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
        /// Delete user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                IActionResult response = null;
                var result = _userInterface.DeleteUser(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User deleted successfully !!!" });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Active / De Active User
        /// </summary>
        /// <param name="id">Unique id of the user</param>
        /// <param name="isActive">Activation status</param>
        /// <returns>Success or not found</returns>
        [HttpPut]
        [Route("users/{id}")]
        public async Task<IActionResult> ActivateDeactivateUser(int id, [FromQuery] bool isActive)
        {
            try
            {
                IActionResult response = null;
                var result = _userInterface.ActivateDeactivateUser(id, isActive);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User updated successfully !!!" });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Assigns a role to a user
        /// </summary>
        /// <param name="id">The unique id of the user</param>
        /// <param name="roleId">The unique id of the role</param>
        /// <returns>Ok or badrequest response</returns>
        [HttpPost]
        [Route("users/{id}/assignrole/{roleId}")]
        public async Task<IActionResult> AssignRole(int id, int roleId)
        {
            try
            {
                IActionResult response = null;

                if(id == 0 || roleId == 0)
                {
                    return response = BadRequest(new { Success = false, Message = "Role and User not selected" });
                }
                var result = _userInterface.AssignRole(id, roleId);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User role assigned successfully !!!" });
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
        /// Removes a role for a user
        /// </summary>
        /// <param name="id">The unique id of the user</param>
        /// <param name="roleId">The unique id of the role</param>
        /// <returns>Ok or badrequest response</returns>
        [HttpDelete]
        [Route("users/{id}/removerole/{roleId}")]
        public async Task<IActionResult> RemoveRole(int id, int roleId)
        {
            try
            {
                IActionResult response = null;

                if (id == 0 || roleId == 0)
                {
                    return response = BadRequest(new { Success = false, Message = "Role and User not selected" });
                }
                var result = _userInterface.RemoveRole(id, roleId);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User role removed successfully !!!" });
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


        #endregion
    }
}

