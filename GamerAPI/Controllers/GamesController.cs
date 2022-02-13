using GamerAPI.Models;
using GamerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GamerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: /<Controller>
        [HttpGet]
        public async Task<ActionResult<GameList>> GetGames([Required]string q, string? sort)
        {
            var games = await _gameService.GetGames(q, sort);

            return Ok(games.Results);
        }

        // GET: /games/12345
        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> GetGame(int gameId)
        {
            var game = await _gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }
    }
}
