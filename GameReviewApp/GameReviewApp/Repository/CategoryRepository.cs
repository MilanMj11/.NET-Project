using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Game> GetGamesByCategory(int categoryId)
        {
            return _context.GameCategories.Where(c => c.CategoryId == categoryId).Select(c => c.Game).ToList();
        }

        bool ICategoryRepository.CreateCategory(Category category)
        {
            if (CategoryExists(category.Id))
                return false;
            _context.Add(category);
            return _context.SaveChanges() > 0;
        }

        bool ICategoryRepository.DeleteCategory(Category category)
        {
            _context.Remove(category);
            return _context.SaveChanges() > 0;
        }

        bool ICategoryRepository.UpdateCategory(Category category)
        {
            _context.Update(category);
            return _context.SaveChanges() > 0;
        }
    }
}
