﻿using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwnerId(int id);
        ICollection<Owner> GetAllOwnersFromCountry(int id);
        bool IsCountryExist(int id);
        bool IsOwnerExist(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(int id, Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}
