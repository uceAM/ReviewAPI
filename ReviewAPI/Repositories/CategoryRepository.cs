using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderByDescending(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Pokemon> GetPokemonByCategoryId(int id)
        {
            return _context.PokemonCategories.Where(pc => pc.CategoryId == id).Select(p=>p.Pokemon).ToList();
        }

        public bool IsCategoryExist(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
}
