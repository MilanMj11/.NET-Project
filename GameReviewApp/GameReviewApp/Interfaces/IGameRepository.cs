using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        Game GetGame(int id);
        Game GetGame(string name);
        decimal GetGameRating(int gameId);
        bool GameExists(int gameId);

        bool CreateGame(int categoryId, int companyId, Game game);
        bool UpdateGame(int gameId);
        bool DeleteGame(int gameId);
        bool DeleteGames(List<Game> gamelist);

    }
}
