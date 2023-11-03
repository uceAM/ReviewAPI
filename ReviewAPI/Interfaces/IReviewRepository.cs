using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsByPokemonId(int id);
        bool IsReviewExist(int id);
        bool IsPokemonExist(int id);
        bool CreateReview(int reviewerId, int pokeId, Review review);
        bool UpdateReview(int reviewId, Review review);
        bool Save();
    }
}
