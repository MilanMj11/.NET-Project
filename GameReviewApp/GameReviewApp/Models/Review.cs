namespace GameReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ReviewerId {  get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Reviewer Reviewer { get; set; }
        public Game Game { get; set; }

    }
}
