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
        public async Task<IActionResult> GetGames([Required]string q, string? sort)
        {
            var games = await _gameService.GetGames(q, sort);
            return Ok(games.Results);
        }
    }
}
