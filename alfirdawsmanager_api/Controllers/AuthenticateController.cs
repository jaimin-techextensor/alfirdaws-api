using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.JwtHelpers;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
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
        private readonly JwtSettings jwtSettings;

        #endregion

        public AuthenticateController(IAuthenticateInterface authenticateInterface, JwtSettings jwtSettings)
        {
            _authenticateInterface = authenticateInterface ?? throw new ArgumentNullException(nameof(authenticateInterface));
            this.jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var Token = new UserTokens();
                var response = await _authenticateInterface.AuthenticateUser(loginRequest.UserName, loginRequest.Password);
                if (response != null)
                {
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        Email = response.Email,
                        UserName = response.UserName,
                        Id = response.UserId,
                    }, jwtSettings);
                }
                else
                {
                    var userResponse = new UserResponse()
                    {
                        Status = "User Not Found",
                        Mesaage = "Invalid UserName or Password"
                    };
                    return Ok(userResponse);
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
