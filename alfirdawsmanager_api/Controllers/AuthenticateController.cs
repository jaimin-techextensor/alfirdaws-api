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
                IActionResult response = null;
                var Token = new UserTokens();
                var result = await _authenticateInterface.AuthenticateUser(loginRequest.UserName, loginRequest.Password);
                if (result != null)
                {
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        Email = result.Email,
                        UserName = result.UserName,
                        Id = result.UserId,
                    }, jwtSettings);
                }
                else
                {
                    //var userResponse = new UserResponse()
                    //{
                    //    Status = "User Not Found",
                    //    Mesaage = "Invalid UserName or Password"
                    //};
                    //return Ok(userResponse);
                    return response = Unauthorized(new { Success = false, Message="Invalid UserName or Password" });
                }
                return response = Ok(new { Success = true, Message = "User Exists", Token});
                //return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                var response = await _authenticateInterface.ForgotPassword(Email);
                if (response != null)
                {
                    var userValidResponse = new UserResponse()
                    {
                        Status = "User Found",
                        Mesaage = "Please check your email to reset your password !!!"
                    };
                    return Ok(userValidResponse);
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
