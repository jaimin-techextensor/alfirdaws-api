using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Members

        private IUserInterface _userInterface;

        #endregion

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
        }

        #region Methods

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

        [HttpGet]
        [Route("searchUsers")]
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

        [HttpPost]
        [Route("createUser")]
        public IActionResult CreateUser([FromQuery]UserModel userModel)
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

        [HttpPost]
        [Route("updateUser")]
        public IActionResult UpdateUser([FromQuery] UserModel userModel)
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

        [HttpPost]
        [Route("deleteUser")]
        public IActionResult DeleteUser(int UserId)
        {
            try
            {
                IActionResult response = null;
                var result = _userInterface.DeleteUser(UserId);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "User deleted successfully !!!" });
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

        #endregion
    }
}

