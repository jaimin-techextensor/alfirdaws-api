using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class InvoiceTypeController : Controller
    {

        #region Members
        private IInvoiceTypeInterface _invoiceTypeInterface;
        #endregion

        #region Constructors
        public InvoiceTypeController(IInvoiceTypeInterface invoiceTypeInterface)
        {
            _invoiceTypeInterface = invoiceTypeInterface ?? throw new ArgumentNullException(nameof(invoiceTypeInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of invoice types
        /// </summary>
        /// <returns>List of invoice types</returns>
        [HttpGet]
        [Route("invoicetypes")]
        public async Task<IActionResult> GetInvoiceTypesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _invoiceTypeInterface.GetInvoiceTypeOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved invoice types overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve invoice types overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves an invoice type by its id
        /// </summary>
        /// <param name="id">Unique id of the invoice type</param>
        /// <returns>Invoice type object</returns>
        [HttpGet]
        [Route("invoicetypes/{id}")]
        public async Task<IActionResult> GetInvoiceTypeById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _invoiceTypeInterface.GetInvoiceTypeById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get invoice type by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve invoice type by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new invoice type
        /// </summary>
        /// <param name="invoiceTypeRequest">The invoice type request</param>
        /// <returns>Actio result with a success or failure</returns>
        [HttpPost]
        [Route("invoicetypes")]
        public async Task<IActionResult> CreateInvoiceType(InvoiceTypeCreateRequest invoiceTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (invoiceTypeRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _invoiceTypeInterface.CreateInvoiceType(invoiceTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Invoice type created successfully !!!", Data = result.Data });
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
        /// Updates a specific invoice type
        /// </summary>
        /// <param name="invoiceTypeRequest">The invoice type request object</param>
        /// <returns>Action result with a success or failure</returns>
        [HttpPut]
        [Route("invoicetypes")]
        public async Task<IActionResult> UpdateInvoiceType(InvoiceTypeUpdateRequest invoiceTypeRequest)
        {
            try
            {
                IActionResult? response = null;
                if (invoiceTypeRequest.InvoiceTypeId == 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _invoiceTypeInterface.UpdateInvoiceType(invoiceTypeRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Invoice type updated successfully !!!", Data = result.Data });
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
        /// Deletes a specific invoice type
        /// </summary>
        /// <param name="id">The unique id of the invoice type</param>
        /// <returns>Action result with a success or failure</returns>
        [HttpDelete]
        [Route("invoicestypes/{id}")]
        public async Task<IActionResult> DeleteInvoiceType(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _invoiceTypeInterface.DeleteInvoiceType(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Invoice type deleted successfully !!!" });
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

