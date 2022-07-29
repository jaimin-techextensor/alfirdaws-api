using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Members

        private IAuthenticateInterface _authenticateInterface;

        #endregion

        public AuthenticateController(IAuthenticateInterface authenticateInterface)
        {
            _authenticateInterface = authenticateInterface ?? throw new ArgumentNullException(nameof(authenticateInterface));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            try
            {
                var response = await _authenticateInterface.AuthenticateUser(UserName, Password);
                if (response == null)
                {
                    var userResponse = new UserResponse()
                    {
                        Status = "User Not Found",
                        Mesaage = "Invalid UserName or Password"
                    };
                    return Ok(userResponse);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
