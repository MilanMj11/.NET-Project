using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }
        public bool ProfileExists(int id)
        {
            return _context.Profiles.Any(p => p.Id == id);
        }
        public ICollection<Profile> GetProfiles()
        {
            return _context.Profiles.ToList();
        }
        public Profile GetProfile(int id)
        {
            return _context.Profiles.Where(p => p.Id == id).FirstOrDefault();
        }
        public bool CreateProfile(Profile profile)
        {
            _context.Add(profile);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteProfile(Profile profile)
        {
            _context.Remove(profile);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateProfile(Profile profile)
        {
            _context.Update(profile);
            return _context.SaveChanges() > 0;
        }
    }
}
