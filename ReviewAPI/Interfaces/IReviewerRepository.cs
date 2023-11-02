using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewerId(int id);
        bool IsReviewerExists(int id);
    }
}
