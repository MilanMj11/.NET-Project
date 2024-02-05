using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }
        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }
        public bool CreateReview(Review review)
        {
            if (ReviewExists(review.Id))
                return false;
            _context.Add(review);
            return _context.SaveChanges() > 0;
        }
        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return _context.SaveChanges() > 0;
        }
        public bool DeleteReviews(List<Review> listReviews)
        {
            _context.RemoveRange(listReviews);
            return _context.SaveChanges() > 0;

        }
    }
}
