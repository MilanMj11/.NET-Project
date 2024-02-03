namespace GameReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
