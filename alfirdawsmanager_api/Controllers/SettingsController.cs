using alfirdawsmanager.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        #region Members

        private ISettingsInterface _settingsInterface;

        #endregion

        public SettingsController(ISettingsInterface settingsInterface)
        {
            _settingsInterface = settingsInterface ?? throw new ArgumentNullException(nameof(settingsInterface));
        }

        #region Methods

        [HttpGet]
        [Route("GetSettingsCounters")]
        public async Task<IActionResult> GetSettingsCounters()
        {
            try
            {
                IActionResult response = null;
                var result = await _settingsInterface.GetSettingsCounters();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved settings counters", Data=result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve settings counters" });
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
