using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class PaymentTypeController : Controller
    {


        #region Members
        private IPaymentTypeInterface _paymentTypeInterface;
        #endregion

        #region Constructors
        public PaymentTypeController(IPaymentTypeInterface paymentTypeInterface)
        {
            _paymentTypeInterface = paymentTypeInterface ?? throw new ArgumentNullException(nameof(paymentTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of the payment types
        /// </summary>
        /// <returns>List of payment types</returns>
        [HttpGet]
        [Route("paymenttypes")]
        public async Task<IActionResult> GetPaymentTypesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _paymentTypeInterface.GetPaymentTypeOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved payment types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve payment types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific payment type
        /// </summary>
        /// <param name="id">The unique id of the payment type</param>
        /// <returns>Action result with true or false</returns>
        [HttpGet]
        [Route("paymenttypes/{id}")]
        public async Task<IActionResult> GetPaymentTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _paymentTypeInterface.GetPaymentTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get payment type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve payment type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new payment type
        /// </summary>
        /// <param name="paymentTypeRequest">The payment type request object</param>
        /// <returns>Action result with true or false</returns>
        [HttpPost]
        [Route("paymenttypes")]
        public async Task<IActionResult> CreatePaymentType(PaymentTypeCreateRequest paymentTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (paymentTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result =  _paymentTypeInterface.CreatePaymentType(paymentTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Payment type created successfully !!!", Data = result.Data });
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
        /// Updates a specific payment type
        /// </summary>
        /// <param name="paymentTypeRequest">The payment type request</param>
        /// <returns>Action result with true or false</returns>
        [HttpPut]
        [Route("paymenttypes")]
        public async Task<IActionResult> UpdatePaymentType(PaymentTypeUpdateRequest paymentTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (paymentTypeRequest.PaymentTypeId == 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _paymentTypeInterface.UpdatePaymentType(paymentTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Payment type updated successfully !!!", Data = result.Data });
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
        /// Deletes a specific payment type
        /// </summary>
        /// <param name="id">The unique id of the payment type</param>
        /// <returns>Action result with true or false</returns>
        [HttpDelete]
        [Route("paymenttypes/{id}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result =  _paymentTypeInterface.DeletePaymentType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Payment Type deleted successfully !!!" });
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

