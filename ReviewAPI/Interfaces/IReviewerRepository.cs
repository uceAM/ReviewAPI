using ReviewAPI.Models;

namespace ReviewAPI.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewerId(int id);
        bool IsReviewerExist(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer( int rvrId, Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
