using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class AddressTypeController : Controller
    {

        #region Members
        private IAddressTypeInterface _addressTypeInterface;
        #endregion

        #region Constructors
        public AddressTypeController(IAddressTypeInterface addressTypeInterface)
        {
            _addressTypeInterface = addressTypeInterface ?? throw new ArgumentNullException(nameof(addressTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all address types
        /// </summary>
        /// <returns>List of address types</returns>
        [HttpGet]
        [Route("addresstypes")]
        public async Task<IActionResult> GetAddressTypeOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _addressTypeInterface.GetAddressTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved address types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve address types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one address type 
        /// </summary>
        /// <param name="id">Unique id of the address type</param>
        /// <returns>address type object</returns>
        [HttpGet]
        [Route("addresstypes/{id}")]
        public async Task<IActionResult> GetAddressTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _addressTypeInterface.GetAddressTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get address type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve address type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new address type 
        /// </summary>
        /// <param name="addressTypeRequest">address type request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("addresstypes")]
        public async Task<IActionResult> CreateAddressType(AddressTypeCreateRequest addressTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (addressTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _addressTypeInterface.CreateAddressType(addressTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Address Type created successfully !!!" });
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
        /// Updates the information of a address type 
        /// </summary>
        /// <param name="addressTypeRequest">Address type request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("addresstypes")]
        public async Task<IActionResult> UpdateAddressType(AddressTypeUpdateRequest addressTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (addressTypeRequest.AddressTypeId == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _addressTypeInterface.UpdateAddressType(addressTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Address Type updated successfully !!!" });
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
        /// Deletes a address type
        /// </summary>
        /// <param name="id">Unique id of the address type that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("addresstypes/{id}")]
        public async Task<IActionResult> DeleteAddressType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _addressTypeInterface.DeleteAddressType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Address Type deleted successfully !!!" });
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

