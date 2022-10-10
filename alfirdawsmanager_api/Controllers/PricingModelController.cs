using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class PricingModelController : Controller
    {

        #region Members
        private IPricingModelInterface _pricingModelInterface;
        #endregion

        #region Constructors
        public PricingModelController(IPricingModelInterface pricingModelInterface)
        {
            _pricingModelInterface = pricingModelInterface ?? throw new ArgumentNullException(nameof(pricingModelInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all pricing models
        /// </summary>
        /// <returns>List of pricing models</returns>
        [HttpGet]
        [Route("pricingmodels")]
        public IActionResult GetPricingModelsOverview([FromQuery] PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                IActionResult? response = null;
                var result = _pricingModelInterface.GetPricingModelsOverview(pageParamsRequestModel);
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
                    return response = Ok(new { Success = true, Message = "Retrieved pricing models overview", PageInfo = metadata, Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve pricing models overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one pricing model 
        /// </summary>
        /// <param name="id">Unique id of the pricing model</param>
        /// <returns>pricing model type object</returns>
        [HttpGet]
        [Route("pricingmodels/{id}")]
        public async Task<IActionResult> GetPricingModelById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _pricingModelInterface.GetPricingModelById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get pricing model by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve pricing model by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new pricing model 
        /// </summary>
        /// <param name="pricingModelRequest">pricing model request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("pricingmodels")]
        public async Task<IActionResult> CreatePricingModel(PricingModelCreateRequest pricingModelRequest)
        {
            try
            {
                IActionResult? response = null;
                if (pricingModelRequest.SubscriptionModelId <= 0 || pricingModelRequest.PeriodTypeId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _pricingModelInterface.CreatePricingModel(pricingModelRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Pricing Model created successfully !!!" });
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
        /// Updates the information of a pricing model 
        /// </summary>
        /// <param name="pricingModelRequest">Pricing model request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("pricingmodels")]
        public async Task<IActionResult> UpdatePricingModel(PricingModelUpdateRequest pricingModelRequest)
        {
            try
            {
                IActionResult? response = null;
                if (pricingModelRequest.SubscriptionModelId <= 0 || pricingModelRequest.PeriodTypeId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _pricingModelInterface.UpdatePricingModel(pricingModelRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Pricing Model updated successfully !!!" });
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
        /// Deletes a pricing model
        /// </summary>
        /// <param name="id">Unique id of the pricing model that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("pricingmodels/{id}")]
        public async Task<IActionResult> DeletePricingModel(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _pricingModelInterface.DeletePricingModel(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Pricing Model deleted successfully !!!" });
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

