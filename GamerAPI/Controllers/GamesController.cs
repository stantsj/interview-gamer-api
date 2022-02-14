using GamerAPI.Models;
using GamerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        public async Task<ActionResult<Games>> GetGames([Required]string q, string? sort)
        {
            var res = await _gameService.GetGamesResponseDTO(q, sort);

            switch (res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.OK:
                    return Ok(res.ReturnObject.Results);
                default:
                    return NotFound();
            }
        }

        // GET: /games/12345
        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameResponseDTO>> GetGame(int gameId)
        { 
            var res = await _gameService.GetGameResponseDTO(gameId);

            switch (res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.OK:
                    return Ok(res.ReturnObject);
                default:
                    return NotFound();
            }
        }
    }
}
