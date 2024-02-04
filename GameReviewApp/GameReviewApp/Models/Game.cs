namespace GameReviewApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }

        public Company Company { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
