using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CampaignTypeController : Controller
    {

        #region Members
        private ICampaignTypeInterface _campaignTypeInterface;
        #endregion

        #region Constructors
        public CampaignTypeController(ICampaignTypeInterface campaignTypeInterface)
        {
            _campaignTypeInterface = campaignTypeInterface ?? throw new ArgumentNullException(nameof(campaignTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all campaign types
        /// </summary>
        /// <returns>List of campaign types</returns>
        [HttpGet]
        [Route("campaigntypes")]
        public async Task<IActionResult> GetCampaignTypeOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _campaignTypeInterface.GetCampaignTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved campaign types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve campaign types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one campaign type 
        /// </summary>
        /// <param name="id">Unique id of the campaign type</param>
        /// <returns>campaign type object</returns>
        [HttpGet]
        [Route("campaigntypes/{id}")]
        public async Task<IActionResult> GetCampaignTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _campaignTypeInterface.GetCampaignTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get campaign type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve campaign type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new campaign type 
        /// </summary>
        /// <param name="campaignTypeRequest">campaign type request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("campaigntypes")]
        public async Task<IActionResult> CreateCampaignType(CampaignTypeCreateRequest campaignTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (campaignTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _campaignTypeInterface.CreateCampaignType(campaignTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Campaign Type created successfully !!!" });
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
        /// Updates the information of a campaign type 
        /// </summary>
        /// <param name="campaignTypeRequest">Campaign type request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("campaigntypes")]
        public async Task<IActionResult> UpdateCampaignType(CampaignTypeUpdateRequest campaignTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (campaignTypeRequest.CampaignTypeId == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _campaignTypeInterface.UpdateCampaignType(campaignTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Campaign Type updated successfully !!!" });
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
        /// Deletes a campaign type
        /// </summary>
        /// <param name="id">Unique id of the campaign type that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("campaigntypes/{id}")]
        public async Task<IActionResult> DeleteCampaignType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _campaignTypeInterface.DeleteCampaignType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Campaign Type deleted successfully !!!" });
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

