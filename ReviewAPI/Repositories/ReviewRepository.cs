using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository( DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderByDescending(r => r.Id).ToList();
        }

        public ICollection<Review> GetReviewsByPokemonId(int id)
        {
            return _context.Reviews.Where(p=> p.Pokemon.Id == id).ToList();
        }

        public bool IsReviewExist(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
        public bool IsPokemonExist(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }

        public bool CreateReview(int reviewerId, int pokeId, Review review)
        {
            var dbReviewer = _context.Reviewers.Where(rvr => rvr.Id == reviewerId).FirstOrDefault();
            var dbPokemon = _context.Pokemons.Where(p =>  p.Id == pokeId).FirstOrDefault();
            if (dbReviewer == null || dbPokemon ==null)
            {
                return false;
            }
            review.Pokemon = dbPokemon;
            review.Reviewer = dbReviewer;
            _context.Add(review);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
