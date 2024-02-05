using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;
        public GameRepository(DataContext context)
        {
            _context = context;
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }

        public Game GetGame(int id)
        {
            return _context.Games.Where(g => g.Id == id).FirstOrDefault();
        }

        public Game GetGame(string name)
        {
            return _context.Games.Where(g => g.Name == name).FirstOrDefault();
        }

        public decimal GetGameRating(int gameId)
        {
            var reviews = _context.Reviews.Where(g => g.Game.Id == gameId);

            if (reviews.Count() <= 0)
                return 0;
            
            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Id).ToList();
        }

        public ICollection<Review> GetReviewsByGameId(int gameId)
        {
            return _context.Reviews.Where(r => r.GameId  == gameId).ToList();
        }

        bool IGameRepository.CreateGame(int categoryId, int companyId, Game game)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var company = _context.Companies.Where(c => c.Id == companyId).FirstOrDefault();
            game.CompanyId = companyId;
            
            var gameCategory = new GameCategory()
            {
                Category = category,
                Game = game
            };

            _context.Add(gameCategory);
            _context.Add(game);
            return _context.SaveChanges() > 0;
        }

        bool IGameRepository.DeleteGame(Game game)
        {
            _context.Remove(game);
            return _context.SaveChanges() > 0;

        }

        bool IGameRepository.DeleteGames(List<Game> gamelist)
        {
            _context.RemoveRange(gamelist);
            return _context.SaveChanges() > 0;
        }

        bool IGameRepository.UpdateGame(Game game)
        {
            _context.Update(game);
            return _context.SaveChanges() > 0;

        }
    }
}
