using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class SubscriptionModelController : Controller
    {

        #region Members
        private ISubscriptionModelInterface _subscriptionModelInterface;
        #endregion

        #region Constructors
        public SubscriptionModelController(ISubscriptionModelInterface subscriptionModelInterface)
        {
            _subscriptionModelInterface = subscriptionModelInterface ?? throw new ArgumentNullException(nameof(subscriptionModelInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all subscription models
        /// </summary>
        /// <returns>List of subscription models</returns>
        [HttpGet]
        [Route("subscriptionmodels")]
        public IActionResult GetSubscriptionModelsOverview([FromQuery] PageParamsRequestModel pageParamsRequestModel)
        {
            try
            {
                IActionResult? response = null;
                var result = _subscriptionModelInterface.GetSubscriptionModelsOverview(pageParamsRequestModel);
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
                    return response = Ok(new { Success = true, Message = "Retrieved subscription models overview", PageInfo = metadata, Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve subscription models overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one subscription model 
        /// </summary>
        /// <param name="id">Unique id of the subscription model</param>
        /// <returns>subscription model type object</returns>
        [HttpGet]
        [Route("subscriptionmodels/{id}")]
        public async Task<IActionResult> GetSubscriptionModelById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _subscriptionModelInterface.GetSubscriptionModelById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get subscription model by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve subscription model by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new subscription model 
        /// </summary>
        /// <param name="subscriptionModelRequest">subscription model request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("subscriptionmodels")]
        public async Task<IActionResult> CreateSubscriptionModel(SubscriptionModelCreateRequest subscriptionModelRequest)
        {
            try
            {
                IActionResult? response = null;
                if (string.IsNullOrEmpty(subscriptionModelRequest.UserType) || string.IsNullOrEmpty(subscriptionModelRequest.SubscriptionType))
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _subscriptionModelInterface.CreateSubscriptionModel(subscriptionModelRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Subscription Model created successfully !!!" });
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
        /// Updates the information of a subscription model 
        /// </summary>
        /// <param name="subscriptionModelRequest">Subscription model request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("subscriptionmodels")]
        public async Task<IActionResult> UpdateSubscriptionModel(SubscriptionModelUpdateRequest subscriptionModelRequest)
        {
            try
            {
                IActionResult? response = null;
                if (subscriptionModelRequest.SubscriptionModelId <= 0 || string.IsNullOrEmpty(subscriptionModelRequest.SubscriptionType) || string.IsNullOrEmpty(subscriptionModelRequest.UserType))
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _subscriptionModelInterface.UpdateSubscriptionModel(subscriptionModelRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Subscription Model updated successfully !!!" });
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
        /// Deletes a subscription model
        /// </summary>
        /// <param name="id">Unique id of the subscription model that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("subscriptionmodels/{id}")]
        public async Task<IActionResult> DeleteSubscriptionModel(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _subscriptionModelInterface.DeleteSubscriptionModel(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Subscription Model deleted successfully !!!" });
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

