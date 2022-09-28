using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class VatTypeController : Controller
    {
        #region Members
        private IVatTypeInterface _vatTypeInterface;
        #endregion

        #region Constructors
        public VatTypeController(IVatTypeInterface vatTypeInterface)
        {
            _vatTypeInterface = vatTypeInterface ?? throw new ArgumentNullException(nameof(vatTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of invoice types
        /// </summary>
        /// <returns>Action result with the success or failure</returns>
        [HttpGet]
        [Route("vattypes")]
        public async Task<IActionResult> GetVatTypesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _vatTypeInterface.GetVATTypesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved vat types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve vat types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Retrieves the VAT type by its ID
        /// </summary>
        /// <param name="id">The unique id of the Vat Type</param>
        /// <returns>Action result with the success or failure to retrieve the data by id</returns>
        [HttpGet]
        [Route("vattypes/{id}")]
        public async Task<IActionResult> GetVatTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _vatTypeInterface.GetVATTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get Vat type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve Vat type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create a new VAT type
        /// </summary>
        /// <param name="vatTypeRequest">The VAT Type request</param>
        /// <returns>Action result with the success or failure of the creation</returns>
        [HttpPost]
        [Route("vattypes")]
        public async Task<IActionResult> CreateVATType(VatTypeCreateRequest vatTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (vatTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _vatTypeInterface.CreateVATType(vatTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Vat Type created successfully !!!", Data = result.Data });
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
        /// Updates a specific VAT type
        /// </summary>
        /// <param name="vatTypeRequest">The VAT type request</param>
        /// <returns>Action result with the success or failure of the update of a VAT type</returns>
        [HttpPut]
        [Route("vattypes")]
        public async Task<IActionResult> UpdateVATType(VatTypeUpdateRequest vatTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (vatTypeRequest.VatTypeId == 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _vatTypeInterface.UpdateVATType(vatTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "VAT Type updated successfully !!!", Data = result.Data });
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
        /// Deletes a specific VAT type
        /// </summary>
        /// <param name="id">The unique id of the VAT type</param>
        /// <returns>Action result with the success or failure of the deletion of the VAT type</returns>
        [HttpDelete]
        [Route("vattypes/{id}")]
        public async Task<IActionResult> DeleteVATType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _vatTypeInterface.DeleteVATType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "VAT Type deleted successfully !!!" });
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

