using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [Route("api")]
    public class ModuleController : Controller
    {
        #region Members

        private IModuleInterface _moduleInterface;

        #endregion

        public ModuleController(IModuleInterface moduleInterface)
        {
            _moduleInterface = moduleInterface ?? throw new ArgumentNullException(nameof(moduleInterface));
        }

        /// <summary>
        /// Gets overview of all modules
        /// </summary>
        /// <returns>List of modules</returns>
        [HttpGet]
        [Route("modules")]
        public async Task<IActionResult> GetModulesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _moduleInterface.GetModulesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved modules overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve modules overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}

