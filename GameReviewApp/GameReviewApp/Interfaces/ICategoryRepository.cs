using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Game> GetGamesByCategory(int categoryId);
        bool CategoryExists(int id);

    }
}
