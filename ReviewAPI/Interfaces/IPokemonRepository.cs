using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
    }
}
