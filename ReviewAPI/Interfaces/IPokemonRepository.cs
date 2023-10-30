using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IPokemonRepository
    {
        IEnumerable<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRatings(int id);
        bool IsPokemonExist(int id);

    }
}
