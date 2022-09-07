using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Service.Service
{
    public class CountryService: ICountryInterface
    {
        private readonly AlfirdawsManagerDbContext _context;
        private readonly IMapper _mapper;

        public CountryService(AlfirdawsManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all the countries from the back-end
        /// </summary>
        /// <returns>List of countries</returns>
        public Task<List<CountryModel>> GetCountriesOverview()
        {
            try
            {
                List<CountryModel> countries = _context.Countries
                                                .Include(s => s.Regions)
                                                .Select(c => new CountryModel
                                                    {
                                                        CountryId = c.CountryId,
                                                        Flag = c.Flag,
                                                        Active = c.Active,
                                                        Icon = c.Icon,
                                                        Name = c.Name,
                                                        CountRegions = c.Regions.Count()
                                                    }
                                                ).ToList();
                return Task.FromResult(countries);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a specific country from the back-end
        /// </summary>
        /// <param name="countryId">The unique id of the country</param>
        /// <returns>Boolean indicating if the deletion was successful or not</returns>
        public bool DeleteCountry(int countryId)
        {
            try
            {
                bool success = false;
                using (var repo = new RepositoryPattern<Country>())
                {
                    var objcountry = _mapper.Map<Country>(repo.SelectByID(countryId));
                    if (objcountry != null)
                    {
                        repo.Delete(countryId);
                        repo.Save();
                        success = true;
                    }
                    return success;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the information of a specific country
        /// </summary>
        /// <param name="countryId">The unique id of the counrty</param>
        /// <returns>The country object</returns>
        public Task<CountryModel> GetCountryById(int countryId)
        {
            try
            {
                CountryModel countrymodel;
                countrymodel = _context.Countries
                                                    .Include(r => r.Regions)
                                                    .Where(c => c.CountryId == countryId)
                                                    .Select(c => new CountryModel
                                                    {
                                                        CountryId = c.CountryId,
                                                        Flag = c.Flag,
                                                        Active = c.Active,
                                                        Icon = c.Icon,
                                                        Name = c.Name,
                                                        CountRegions = c.Regions.Count()
                                                    }
                                                    ).SingleOrDefault();

                return Task.FromResult(countrymodel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new country in the back-end
        /// </summary>
        /// <param name="countryReq">The country request with all its data</param>
        /// <returns>Indication if the creation is successfull or not</returns>
        public bool CreateCountry(CountryRequest countryReq)
        {
            try
            {
                bool success = false;

                var country = new Country();
                country.Name = countryReq.Name;
                country.Icon = countryReq.Icon;
                country.Active = countryReq.Active;

                using (var repo = new RepositoryPattern<Country>())
                {
                    repo.Insert(country);
                    repo.Save();
                    success = true;
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the information of a specific country
        /// </summary>
        /// <param name="countryReq">The country request with all its data</param>
        /// <returns>Indication if the update of the country is successful or not</returns>
        public bool UpdateCountry(int countryId, CountryRequest countryReq)
        {
            try
            {
                bool success = false;

                var country = _context.Countries.Where(c => c.CountryId == countryId).SingleOrDefault();
                if (country != null)
                {
                    if (country.Name != null) country.Name = countryReq.Name;
                    if (country.Icon != null) country.Icon = countryReq.Icon;
                    if (country.Active != null) country.Active = countryReq.Active;

                    using (var repo = new RepositoryPattern<Country>())
                    {
                        repo.Update(country);
                        repo.Save();
                        success = true;
                    }
                }
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

