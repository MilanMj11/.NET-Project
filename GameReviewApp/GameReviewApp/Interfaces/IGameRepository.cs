using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
    }
}
