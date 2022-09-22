using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api")]
    [ApiController]
    public class CategoryController : Controller
    {

        #region Members

        private ICategoryInterface _categoryInterface;

        #endregion

        #region Constructors

        public CategoryController(ICategoryInterface categoryInterface)
        {
            _categoryInterface = categoryInterface ?? throw new ArgumentNullException(nameof(categoryInterface));
        }

        #endregion



        /// <summary>
        /// Retrieves the overview of all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategoriesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _categoryInterface.GetCategoriesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved categories overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve categories overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets information of one category 
        /// </summary>
        /// <param name="id">Unique id of the category</param>
        /// <returns>Category object</returns>
        [HttpGet]
        [Route("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _categoryInterface.GetCategoryById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get category by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve category by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Create new category 
        /// </summary>
        /// <param name="catRequest">Category request object</param>
        /// <returns>Ok or Badrequest</returns>
        [HttpPost]
        [Route("categories")]
        public async Task<IActionResult> CreateCategory(CategoryCreateRequest catRequest)
        {
            try
            {
                IActionResult? response = null;
                if (catRequest.Name == null )
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _categoryInterface.CreateCategory(catRequest);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Category created successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates the information of a category 
        /// </summary>
        /// <param name="catRequest">Category request opbject</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("categories")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequest catRequest)
        {
            try
            {
                IActionResult? response = null;
                if (catRequest.CategoryId == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _categoryInterface.UpdateCategory(catRequest);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Category updated successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Deletes a category
        /// </summary>
        /// <param name="id">Unique id of the category that needs to be deleted</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _categoryInterface.DeleteCategory(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Category deleted successfully !!!" });
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



        /// <summary>
        /// Gets the overview of subcategories for a given category 
        /// </summary>
        /// <param name="id">Unique id of the category</param>
        /// <returns>List of subcategories</returns>
        [HttpGet]
        [Route("categories/{id}/subcategories")]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _categoryInterface.GetSubCategories(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get subcategories for category retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve subcategories for given category" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Creates a new Subcategory for a specific category
        /// </summary>
        /// <param name="id">The id of category for which the subcategory is created</param>
        /// <param name="subCatRequest">The subcategory request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("categories/{id}/subcategories")]
        public async Task<IActionResult> CreateSubCategory(int id, SubCategoryCreateRequest subCatRequest)
        {
            try
            {
                IActionResult? response = null;
                if (subCatRequest.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _categoryInterface.CreateSubCategory(id, subCatRequest);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "SubCategory created successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("categories/{id}/subcategories")]
        public async Task<IActionResult> UpdateSubCategory(int id, SubCategoryUpdateRequest subCatRequest)
        {
            try
            {
                IActionResult? response = null;
                if (subCatRequest.SubCategoryId == 0)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _categoryInterface.UpdateSubCategory(id, subCatRequest);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Category updated successfully !!!" });
                }
                else
                {
                    return response = BadRequest(new { Success = false, Message = "Something went wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// Gets a specific subcategory
        /// </summary>
        /// <param name="id">Unique id of the subcategory</param>
        /// <returns>Subcategory object</returns>
        [HttpGet]
        [Route("categories/subcategories/{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _categoryInterface.GetSubCategoryById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get subcategory retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve subcategory" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Deletes a specific subcategory
        /// </summary>
        /// <param name="id">The unique id of the subcategory</param>
        /// <returns>Ok or bad request</returns>
        [HttpDelete]
        [Route("categories/subcategories/{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _categoryInterface.DeleteSubCategory(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "SubCategory deleted successfully !!!" });
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

