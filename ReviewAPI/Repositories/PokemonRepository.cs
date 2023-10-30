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

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRatings(int id)
        {
            var reviewResult = _context.Reviews.Where(r => r.Pokemon.Id == id);
            if(reviewResult.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)reviewResult.Sum(r => r.Rating) / reviewResult.Count());

        }

        public IEnumerable<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderByDescending(p => p.Id).ToList();
        }

        public bool IsPokemonExist(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }
    }
}
