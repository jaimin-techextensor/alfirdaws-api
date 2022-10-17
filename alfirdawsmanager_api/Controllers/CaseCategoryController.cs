using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CaseCategoryController : Controller
    {

        #region Members
        private ICaseCategoryInterface _caseCategoryInterface;
        #endregion

        #region Constructors
        public CaseCategoryController(ICaseCategoryInterface caseCategoryInterface)
        {
            _caseCategoryInterface = caseCategoryInterface ?? throw new ArgumentNullException(nameof(caseCategoryInterface));
        }
        #endregion

        /// <summary>
        /// Retrieves the overview of all case category
        /// </summary>
        /// <returns>List of case category</returns>
        [HttpGet]
        [Route("casecategory")]
        public async Task<IActionResult> GetCaseCategoryOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _caseCategoryInterface.GetCaseCategoryOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved case category overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case category overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets information of one case category 
        /// </summary>
        /// <param name="id">Unique id of the case category</param>
        /// <returns>case category object</returns>
        [HttpGet]
        [Route("casecategory/{id}")]
        public async Task<IActionResult> GetCaseCategoryById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _caseCategoryInterface.GetCaseCategoryById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get case category by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve case category by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new case category 
        /// </summary>
        /// <param name="caseCategoryRequest">case category request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("casecategory")]
        public async Task<IActionResult> CreateCaseCategory(CaseCategoryCreateRequest caseCategoryRequest)
        {
            try
            {
                IActionResult? response = null;
                if (caseCategoryRequest.Name == null)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _caseCategoryInterface.CreateCaseCategory(caseCategoryRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case Category created successfully !!!", Data = result.Data });
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
        /// Updates the information of a case type 
        /// </summary>
        /// <param name="caseCategoryRequest">Case category request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("casecategory")]
        public async Task<IActionResult> UpdateCaseCategory(CaseCategoryUpdateRequest caseCategoryRequest)
        {
            try
            {
                IActionResult? response = null;
                if (caseCategoryRequest.CaseCategoryId == null || caseCategoryRequest.CaseCategoryId <= 0)
                {
                    return response = Ok(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _caseCategoryInterface.UpdateCaseCategory(caseCategoryRequest);
                if (result.Success == true)
                {
                    return response = Ok(new { Success = result.Success, Message = "Case Category updated successfully !!!", Data = result.Data });
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
        /// Deletes a case category
        /// </summary>
        /// <param name="id">Unique id of the case category that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("casecategory/{id}")]
        public async Task<IActionResult> DeleteCaseCategory(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _caseCategoryInterface.DeleteCaseCategory(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Case Category deleted successfully !!!" });
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
