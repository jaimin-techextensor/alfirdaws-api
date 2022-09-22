using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CampaignController : Controller
    {

        #region Members
        private ICampaignInterface _campaignInterface;
        #endregion

        #region Constructors
        public CampaignController(ICampaignInterface campaignInterface)
        {
            _campaignInterface = campaignInterface ?? throw new ArgumentNullException(nameof(campaignInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all campaigns
        /// </summary>
        /// <returns>List of campaigns</returns>
        [HttpGet]
        [Route("campaigns")]
        public IActionResult GetCampaignsOverview([FromQuery] PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                IActionResult? response = null;
                var result = _campaignInterface.GetCampaignsOverview(pageParamsRequestModel);
                if (result != null)
                {
                    var metadata = new
                    {
                        result.TotalCount,
                        result.PageSize,
                        result.CurrentPage,
                        result.TotalPages,
                        result.HasNext,
                        result.HasPrevious
                    };
                    return response = Ok(new { Success = true, Message = "Retrieved campaigns overview", PageInfo = metadata, Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve campaigns overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one campaign 
        /// </summary>
        /// <param name="id">Unique id of the campaign</param>
        /// <returns>campaign type object</returns>
        [HttpGet]
        [Route("campaigns/{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _campaignInterface.GetCampaignById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get campaign by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve campaign by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new campaign 
        /// </summary>
        /// <param name="campaignRequest">campaign request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("campaigns")]
        public async Task<IActionResult> CreateCampaign(CampaignCreateRequest campaignRequest)
        {
            try
            {
                IActionResult? response = null;
                if (campaignRequest.CampaignTypeId <= 0 || campaignRequest.ReachTypeId <= 0 || campaignRequest.PeriodTypeId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _campaignInterface.CreateCampaign(campaignRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Campaign created successfully !!!" });
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
        /// Updates the information of a campaign 
        /// </summary>
        /// <param name="campaignRequest">Campaign request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("campaigns")]
        public async Task<IActionResult> UpdateCampaign(CampaignUpdateRequest campaignRequest)
        {
            try
            {
                IActionResult? response = null;
                if (campaignRequest.CampaignTypeId <= 0 || campaignRequest.ReachTypeId <= 0 || campaignRequest.PeriodTypeId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _campaignInterface.UpdateCampaign(campaignRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Campaign updated successfully !!!" });
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
        /// Deletes a campaign
        /// </summary>
        /// <param name="id">Unique id of the campaign that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("campaigns/{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _campaignInterface.DeleteCampaign(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Campaign deleted successfully !!!" });
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

