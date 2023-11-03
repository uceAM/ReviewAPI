using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRatings(int id);
        bool IsPokemonExist(int id);
        bool CreatePokemon(int ownerId, int catId, Pokemon pokemon);
        bool UpdatePokemon(int pokeId, Pokemon pokemon);
        bool Save();

    }
}
