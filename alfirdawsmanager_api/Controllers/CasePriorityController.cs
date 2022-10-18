using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CasePriorityController : Controller
    {
        #region Members
        private ICasePriorityInterface _casePriorityInterface;
        #endregion

        #region Constructors
        public CasePriorityController(ICasePriorityInterface casePriorityInterface)
        {
            _casePriorityInterface = casePriorityInterface ?? throw new ArgumentNullException(nameof(casePriorityInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all case priority
        /// </summary>
        /// <returns>List of case priority</returns>
        [HttpGet]
        [Route("casepriority")]
        public async Task<IActionResult> GetCasePriorityOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _casePriorityInterface.GetCasePriorityOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved case priority overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case priority overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets information of one case priority 
        /// </summary>
        /// <param name="id">Unique id of the case priority</param>
        /// <returns>case priority object</returns>
        [HttpGet]
        [Route("casepriority/{id}")]
        public async Task<IActionResult> GetCasePriorityById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _casePriorityInterface.GetCasePriorityById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get case priority by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case priority by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new case priority 
        /// </summary>
        /// <param name="casePriorityRequest">case priority request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("casepriority")]
        public async Task<IActionResult> CreateCasePriority(CasePriorityCreateRequest casePriorityRequest)
        {
            try
            {
                IActionResult? response = null;
                if (casePriorityRequest.Name == null || casePriorityRequest.Color == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _casePriorityInterface.CreateCasePriority(casePriorityRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case priority created successfully !!!", Data = result.Data });
                }
                else if (!string.IsNullOrEmpty(result.Message))
                {
                    return response = Ok(new { Success = result.Success, Message = result.Message });
                }
                else
                {
                    return response = BadRequest(new { Success = result.Success, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the information of a case priority 
        /// </summary>
        /// <param name="casePriorityRequest">Case priority request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("casepriority")]
        public async Task<IActionResult> UpdateCasePriority(CasePriorityUpdateRequest casePriorityRequest)
        {
            try
            {
                IActionResult? response = null;
                if (casePriorityRequest.Name == null || casePriorityRequest.Color == null || casePriorityRequest.CasePriorityId == null || casePriorityRequest.CasePriorityId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _casePriorityInterface.UpdateCasePriority(casePriorityRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case priority updated successfully !!!", Data = result.Data });
                }
                else if (!string.IsNullOrEmpty(result.Message))
                {
                    return response = Ok(new { Success = result.Success, Message = result.Message });
                }
                else
                {
                    return response = BadRequest(new { Success = result.Success, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a case priority
        /// </summary>
        /// <param name="id">Unique id of the case priority that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("casepriority/{id}")]
        public async Task<IActionResult> DeleteCasePriority(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _casePriorityInterface.DeleteCasePriority(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Case priority deleted successfully !!!" });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
