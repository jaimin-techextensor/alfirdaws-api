using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
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
        public async Task<IActionResult> GetUsersOverview()
        {
            try
            {
                IActionResult response = null;
                var result = await _userInterface.GetUsersOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved users overview", Data = result });
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
        public async Task<IActionResult> SearchUsers([FromQuery]SearchUsersRequest searchUsersRequest)
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
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                IActionResult response = null;
                var result = await _userInterface.GetUserById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get user by Id retrieved", Data = result });
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
        public async Task<IActionResult> CreateUser([FromForm]UserModel userModel)
        {
            try
            {
                IActionResult response = null;
                if(userModel.UserName==null || userModel.Name==null || userModel.LastName==null || userModel.Email ==null || userModel.Password == null)
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
        public async Task<IActionResult> UpdateUser([FromForm]UserModel userModel)
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

        #endregion
    }
}

