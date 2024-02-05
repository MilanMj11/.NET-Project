﻿using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
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

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
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

    }
}
