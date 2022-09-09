using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models.RequestModels;
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
        /// Retrieves a specific country
        /// </summary>
        /// <param name="id">The unique id of the country</param>
        /// <returns>Country object</returns>
        [HttpGet]
        [Route("countries/{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = await _countryInterface.GetCountryById(id);
                if (result != null)
                {
                    return response = Ok(new { Success = true, Message = "Get country by Id retrieved", Data = result });
                }
                else
                {
                    return response = NotFound(new { Success = false, Message = "Could not retrieve country by Id" });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Creates a new country
        /// </summary>
        /// <param name="countryReq">Country request object</param>
        /// <returns>Ok or bad request</returns>
        [HttpPost]
        [Route("countries")]
        public async Task<IActionResult> CreateCountry(CountryRequest countryReq)
        {
            try
            {
                IActionResult? response = null;
                if (countryReq.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _countryInterface.CreateCountry(countryReq);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Country created successfully !!!" });
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
        /// Update an existing country
        /// </summary>
        /// <param name="catRequest">Country update request object</param>
        /// <returns>Ok or bad request</returns>
        [HttpPut]
        [Route("countries/{id}")]
        public async Task<IActionResult> UpdateCountry(int id ,CountryRequest countryReq)
        {
            try
            {
                IActionResult? response = null;
                if (countryReq.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _countryInterface.UpdateCountry(id, countryReq);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Country updated successfully !!!" });
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


        /// <summary>
        /// Creates a region for a specific country
        /// </summary>
        /// <param name="id">The unique id of the country</param>
        /// <param name="regionReq">The region informatioin</param>
        /// <returns>Success or failure result</returns>
        [HttpPost]
        [Route("countries/{id}/regions")]
        public async Task<IActionResult> CreateRegion(int id, RegionRequest regionReq)
        {
            try
            {
                IActionResult? response = null;
                if (regionReq.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _countryInterface.CreateRegion(id, regionReq);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Region created successfully !!!" });
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
        /// Updates the information of a specific region
        /// </summary>
        /// <param name="id">The unique id of the country</param>
        /// <param name="regionId">The unique id of the region</param>
        /// <param name="regionRequest">The region information</param>
        /// <returns>Success or failure result</returns>
        [HttpPut]
        [Route("countries/{id}/regions/{regionId}")]
        public async Task<IActionResult> UpdateRegion(int id, int regionId, RegionRequest regionRequest)
        {
            try
            {
                IActionResult? response = null;
                if (regionRequest.Name == null)
                {
                    return response = BadRequest(new { Success = false, Message = "Please fill in the required fields" });
                }
                var result = _countryInterface.UpdateRegion(id, regionId, regionRequest);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Region updated successfully !!!" });
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
        /// Deletes a specific region
        /// </summary>
        /// <param name="id">Unique id of the region</param>
        /// <returns>Success or failure result</returns>
        [HttpDelete]
        [Route("countries/regions/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                IActionResult? response = null;
                var result = _countryInterface.DeleteRegion(id);
                if (result == true)
                {
                    return response = Ok(new { Success = true, Message = "Region deleted successfully !!!" });
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

