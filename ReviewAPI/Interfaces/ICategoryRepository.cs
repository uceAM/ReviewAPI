using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategoryId(int id);
        bool IsCategoryExist(int id);
    }
}
