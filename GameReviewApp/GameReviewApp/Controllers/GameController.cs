using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public GameController(IGameRepository gameRepository, IMapper mapper,IReviewRepository reviewRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        public IActionResult GetGames()
        {
            var games = _mapper.Map<List<GameDto>>(_gameRepository.GetGames());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(games);
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var game = _mapper.Map<GameDto>(_gameRepository.GetGame(gameId));

            if(!ModelState.IsValid)
                return BadRequest();
            
            return Ok(game);
        }

        [HttpGet("{gameId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetGameRating(int gameId)
        {
            if(!_gameRepository.GameExists(gameId))
                return NotFound();

            var rating = _gameRepository.GetGameRating(gameId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGame([FromQuery] int categoryId, [FromQuery] int companyId, [FromBody] GameDto gameCreate)
        {
            if (gameCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var game = _mapper.Map<Game>(gameCreate);

            if (!_gameRepository.CreateGame(categoryId, companyId, game))
                return BadRequest("Cannot create game.");

            return Ok("Game successfully created.");
        }

        [HttpPut("{gameId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateGame(int gameId, [FromBody] GameDto gameUpdate)
        {
            if (gameUpdate == null)
                return BadRequest(ModelState);

            if (!_gameRepository.GameExists(gameId))
                return BadRequest("Game does not exist.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var realgame = _gameRepository.GetGame(gameId);
            realgame.Name = gameUpdate.Name;
            realgame.Difficulty = gameUpdate.Difficulty;

            // var game = _mapper.Map<Game>(gameUpdate);
           

            if (!_gameRepository.UpdateGame(realgame))
            {
                ModelState.AddModelError("", "Error updating game.");
                return StatusCode(500, ModelState);
            }

            return Ok("Game successfully updated.");
        }


        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var reviews = _gameRepository.GetReviewsByGameId(gameId).ToList();
            var game = _gameRepository.GetGame(gameId);

            if (!_reviewRepository.DeleteReviews(reviews))
            {
                ModelState.AddModelError("", "Error deleting reviews.");
                return StatusCode(500, ModelState);
            }

            if (!_gameRepository.DeleteGame(game))
            {
                ModelState.AddModelError("", "Error deleting game.");
                return StatusCode(500, ModelState);
            }

            return Ok("Game successfully deleted.");

        }

    }
}
