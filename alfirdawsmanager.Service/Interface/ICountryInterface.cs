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

        bool CreateRegion(int countryId, RegionRequest regionReq);
        bool UpdateRegion(int countryId, int regionId, RegionRequest regionRequest);
        bool DeleteRegion(int regionId);
    }
}

