using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PeriodTypeController : Controller
    {

        #region Members
        private IPeriodTypeInterface _periodTypeInterface;
        #endregion

        #region Constructors
        public PeriodTypeController(IPeriodTypeInterface periodTypeInterface)
        {
            _periodTypeInterface = periodTypeInterface ?? throw new ArgumentNullException(nameof(periodTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all period types
        /// </summary>
        /// <returns>List of Period types</returns>
        [HttpGet]
        [Route("periodtypes")]
        public async Task<IActionResult> GetPeriodTypeOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _periodTypeInterface.GetPeriodTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved period types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve period types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one period type 
        /// </summary>
        /// <param name="id">Unique id of the period type</param>
        /// <returns>period type object</returns>
        [HttpGet]
        [Route("periodtypes/{id}")]
        public async Task<IActionResult> GetPeriodTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _periodTypeInterface.GetPeriodTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get period type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve period type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new period type 
        /// </summary>
        /// <param name="periodTypeRequest">period type request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("periodtypes")]
        public async Task<IActionResult> CreatePeriodType(PeriodTypeCreateRequest periodTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (periodTypeRequest.Name == null || (periodTypeRequest.NrOfDays == null || periodTypeRequest.NrOfDays <= 0))
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _periodTypeInterface.CreatePeriodType(periodTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Period Type created successfully !!!" });
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
        /// Updates the information of a period type 
        /// </summary>
        /// <param name="periodTypeRequest">Period type request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("periodtypes")]
        public async Task<IActionResult> UpdatePeriodType(PeriodTypeUpdateRequest periodTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (periodTypeRequest.PeriodTypeId == null || periodTypeRequest.NrOfDays <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _periodTypeInterface.UpdatePeriodType(periodTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Period Type updated successfully !!!" });
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
        /// Deletes a period type
        /// </summary>
        /// <param name="id">Unique id of the period type that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("periodtypes/{id}")]
        public async Task<IActionResult> DeletePeriodType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _periodTypeInterface.DeletePeriodType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Period Type deleted successfully !!!" });
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

