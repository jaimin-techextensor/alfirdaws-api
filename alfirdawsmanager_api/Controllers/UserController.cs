using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [Route("api/[controller]")]
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
        [Route("GetUsersOverview")]
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
        [Route("SearchUsers")]
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

        #endregion
    }
}
