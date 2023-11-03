using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCountry(Country country)
        {
           _context.Add(country);
            return Save();
        }

        public ICollection<Owner> GetAllOwnersFromCountry(int id)
        {
            return _context.Owners.Where(c => c.Country.Id == id).ToList();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderByDescending(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwnerId(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public bool IsCountryExist(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCountry(int id, Country country)
        {
            _context.Update(country);
            return Save();
        }

        bool ICountryRepository.IsOwnerExist(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }
    }
}
