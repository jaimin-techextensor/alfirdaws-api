using System;
using alfirdawsmanager.Service.Models;
using alfirdawsmanager.Service.Models.RequestModels;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICountryInterface
    {
        Task<List<CountryModel>> GetCountriesOverview();
        Task<CountryModel> GetCountryById(int countryId);
        bool CreateCountry(CountryRequest countryReq);
        bool UpdateCountry(int countryId, CountryRequest countryReq);
        bool DeleteCountry(int countryId);
    }
}

