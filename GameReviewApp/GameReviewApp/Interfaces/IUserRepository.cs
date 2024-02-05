using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        bool UserExistsById(int id);
        bool UserExistsByUsername(string username);
        bool CreateUser(User user);

    }
}
