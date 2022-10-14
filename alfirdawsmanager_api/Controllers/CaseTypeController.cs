using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CaseTypeController : Controller
    {

        #region Members
        private ICaseTypeInterface _caseTypeInterface;
        #endregion

        #region Constructors
        public CaseTypeController(ICaseTypeInterface caseTypeInterface)
        {
            _caseTypeInterface = caseTypeInterface ?? throw new ArgumentNullException(nameof(caseTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all case types
        /// </summary>
        /// <returns>List of case types</returns>
        [HttpGet]
        [Route("casetypes")]
        public async Task<IActionResult> GetCaseTypeOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _caseTypeInterface.GetCaseTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved case types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets information of one case type 
        /// </summary>
        /// <param name="id">Unique id of the case type</param>
        /// <returns>case type object</returns>
        [HttpGet]
        [Route("casetypes/{id}")]
        public async Task<IActionResult> GetCaseTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _caseTypeInterface.GetCaseTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get case type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new case type 
        /// </summary>
        /// <param name="caseTypeRequest">case type request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("casetypes")]
        public async Task<IActionResult> CreateCaseType(CaseTypeCreateRequest caseTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (caseTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _caseTypeInterface.CreateCaseType(caseTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case Type created successfully !!!", Data = result.Data });
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
        /// Updates the information of a case type 
        /// </summary>
        /// <param name="caseTypeRequest">Case type request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("casetypes")]
        public async Task<IActionResult> UpdateCaseType(CaseTypeUpdateRequest caseTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (caseTypeRequest.CaseTypeId == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _caseTypeInterface.UpdateCaseType(caseTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case Type updated successfully !!!", Data = result.Data });
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
        /// Deletes a case type
        /// </summary>
        /// <param name="id">Unique id of the case type that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("casetypes/{id}")]
        public async Task<IActionResult> DeleteCaseType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _caseTypeInterface.DeleteCaseType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Case Type deleted successfully !!!" });
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

