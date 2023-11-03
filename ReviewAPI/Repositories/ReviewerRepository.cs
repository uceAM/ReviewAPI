using ReviewAPI.Data;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(rvr=>rvr.Id ==id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderByDescending(rvr => rvr.Id).ToList();
        }

        public ICollection<Review> GetReviewsByReviewerId(int id)
        {
            return _context.Reviewers.Where(rvr => rvr.Id == id).SelectMany(r => r.Reviews).ToList();
            //return _context.Reviews.Where(r=> r.Reviewer.Id == id).ToList();
        }

        public bool IsReviewerExists(int id)
        {
            return _context.Reviewers.Any(rvr => rvr.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateReviewer(int rvrId, Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}
