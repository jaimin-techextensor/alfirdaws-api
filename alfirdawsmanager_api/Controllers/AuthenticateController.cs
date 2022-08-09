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

        #region Methods

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
                    return response = Unauthorized(new { Success = false, Message = "Invalid UserName or Password" });
                }
                return response = Ok(new { Success = true, Message = "User Exists", Token });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            try
            {
                IActionResult response = null;
                var result = await _authenticateInterface.ForgotPassword(forgotPasswordRequest.Email);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Please check your email to reset your password !!!" });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Invalid Email" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                IActionResult response = null;
                var result = await _authenticateInterface.ResetPassword(resetPasswordRequest.Email, resetPasswordRequest.Password);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Password changed successfully !!!" });
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
