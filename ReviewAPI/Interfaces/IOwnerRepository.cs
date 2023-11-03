using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        Owner GetOwnerByPokemonId(int id);
        ICollection<Pokemon> GetPokemonsByOwnerId(int id);
        bool IsOwnerExist(int id);
        bool IsPokemonExist(int id);
        bool CreateOwner( int countryId,Owner owner);
        bool UpdateOwner( int ownerId, Owner owner);
        bool Save();
    }
}
