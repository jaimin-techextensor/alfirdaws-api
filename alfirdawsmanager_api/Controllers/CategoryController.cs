using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace alfirdawsmanager_api.Controllers
{
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



    }
}

