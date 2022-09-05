using System;
using alfirdawsmanager.Service.Models;

namespace alfirdawsmanager.Service.Interface
{
    public interface ICountryInterface
    {
        Task<List<CountryModel>> GetCountriesOverview();
        bool DeleteCountry(int countryId);
    }
}

