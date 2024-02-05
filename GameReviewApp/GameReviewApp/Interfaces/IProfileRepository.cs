using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IProfileRepository
    {
        ICollection<Profile> GetProfiles();
        Profile GetProfile(int id);
        bool ProfileExists(int id);
        bool CreateProfile(Profile profile);
        bool UpdateProfile(Profile profile);
        bool DeleteProfile(Profile profile);
    }
}
