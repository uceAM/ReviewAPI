using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderByDescending(p => p.Id).ToList();
        }
    }
}
