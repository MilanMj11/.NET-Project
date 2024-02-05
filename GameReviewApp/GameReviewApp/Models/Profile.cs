namespace GameReviewApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Reputation { get; set; }
        public Reviewer Reviewer { get; set; }
        public User User { get; set; }
    }
}
