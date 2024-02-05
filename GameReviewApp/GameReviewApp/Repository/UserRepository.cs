using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool UserExistsById(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }
        
        public bool CreateUser(User user)
        {
            _context.Add(user);
            if(_context.SaveChanges() <= 0)
                return false;
            return true;
        }
    }
}
