namespace GameReviewApp.Models
{
    public enum RolType
    {
        Admin,
        User
    }
    public class User
    {
        public int Id { get; set; }
        public RolType Rol { get; set; }  
        public string Username { get; set; } = string.Empty;
        public Profile Profile { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;}
    }
}
