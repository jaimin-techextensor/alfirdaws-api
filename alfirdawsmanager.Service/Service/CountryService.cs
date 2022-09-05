using System;
using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Data.Repository;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Models;
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
    }
}

