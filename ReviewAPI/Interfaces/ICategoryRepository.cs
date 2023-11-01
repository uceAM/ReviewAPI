using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        IEnumerable<Pokemon> GetPokemonByCategoryId(int id);
        bool IsCategoryExist(int id);
    }
}
