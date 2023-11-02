using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class OwnerRepository:IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext dataContext)
        {
            _context = dataContext;

        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public Owner GetOwnerByPokemonId(int id)
        {
            return _context.PokemonOwners.Where(po => po.PokemonId == id).Select(o => o.Owner).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderByDescending(o => o.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonsByOwnerId(int id)
        {
            return _context.PokemonOwners.Where(po => po.OwnerId == id).Select(p => p.Pokemon).ToList(); 
        }

        public bool IsOwnerExist(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }

        public bool IsPokemonExist(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }
    }
}
