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

        public bool CreatePokemon(int ownerId, int catId, Pokemon pokemon)
        {
            var dbOwner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var dbCategory = _context.Categories.Where(c => c.Id == catId).FirstOrDefault();
            if(dbOwner == null || dbCategory == null)
            {
                return false;
            }
            var createPokeOwnr = new PokemonOwner()
            {
                Owner = dbOwner,
                Pokemon = pokemon,
            };
            _context.Add(createPokeOwnr);
            var createPokeCat = new PokemonCategory()
            {
                Category = dbCategory,
                Pokemon = pokemon,
            };
            _context.Add(createPokeCat);
            _context.Add(pokemon);
            return Save();
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

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderByDescending(p => p.Id).ToList();
        }

        public bool IsPokemonExist(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
