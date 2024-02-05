using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        ICollection<Review> GetReviewsByGameId(int gameId);
        Game GetGame(int id);
        Game GetGame(string name);
        decimal GetGameRating(int gameId);
        bool GameExists(int gameId);

        bool CreateGame(int categoryId, int companyId, Game game);
        bool UpdateGame(Game game);
        bool DeleteGame(Game game);
        bool DeleteGames(List<Game> gamelist);

    }
}
