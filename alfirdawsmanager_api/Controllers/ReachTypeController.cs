using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReachTypeController : Controller
    {

        #region Members
        private IReachTypeInterface _reachTypeInterface;
        #endregion

        #region Constructors
        public ReachTypeController(IReachTypeInterface reachTypeInterface)
        {
            _reachTypeInterface = reachTypeInterface ?? throw new ArgumentNullException(nameof(reachTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all reach types
        /// </summary>
        /// <returns>List of Reach types</returns>
        [HttpGet]
        [Route("reachtypes")]
        public async Task<IActionResult> GetReachTypeOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _reachTypeInterface.GetReachTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved reach types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve reach types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one reach type 
        /// </summary>
        /// <param name="id">Unique id of the reach type</param>
        /// <returns>reach type object</returns>
        [HttpGet]
        [Route("reachtypes/{id}")]
        public async Task<IActionResult> GetReachTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _reachTypeInterface.GetReachTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get reach type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve reach type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new reach type 
        /// </summary>
        /// <param name="reachTypeRequest">reach type request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("reachtypes")]
        public async Task<IActionResult> CreateReachType(ReachTypeCreateRequest reachTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (reachTypeRequest.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _reachTypeInterface.CreateReachType(reachTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Reach Type created successfully !!!" });
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
        /// Updates the information of a reach type 
        /// </summary>
        /// <param name="reachTypeRequest">Reach type request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("reachtypes")]
        public async Task<IActionResult> UpdateReachType(ReachTypeUpdateRequest reachTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (reachTypeRequest.ReachTypeId == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _reachTypeInterface.UpdateReachType(reachTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Reach Type updated successfully !!!" });
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
        /// Deletes a reach type
        /// </summary>
        /// <param name="id">Unique id of the reach type that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("reachtypes/{id}")]
        public async Task<IActionResult> DeleteReachType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _reachTypeInterface.DeleteReachType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Reach Type deleted successfully !!!" });
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

