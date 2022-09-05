using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace alfirdawsmanager_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CountryController : Controller
    {
        private ICountryInterface _countryInterface;

        public CountryController(ICountryInterface countryInterface)
        {
            _countryInterface = countryInterface ?? throw new ArgumentNullException(nameof(countryInterface));
        }

        /// <summary>
        /// Retrieves the overview of all countries
        /// </summary>
        /// <returns>List of countries</returns>
        [HttpGet]
        [Route("countries")]
        public async Task<IActionResult> GetCountriesOverview()
        {
            try
            {
                IActionResult? response = null;
                var result = await _countryInterface.GetCountriesOverview();
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Retrieved countries overview", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve countries overview" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a specific country 
        /// </summary>
        /// <param name="id">The unique id of the country</param>
        /// <returns>Success or failure result</returns>
        [HttpDelete]
        [Route("countries/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _countryInterface.DeleteCountry(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Country deleted successfully !!!" });
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

