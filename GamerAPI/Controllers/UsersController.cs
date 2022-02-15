#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamerAPI.Models;
using GamerAPI.Services;
using System.Net;

namespace GamerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _userService.GetUsers();

            return res.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.OK => Ok(res.ReturnObject),
                _ => NotFound(),
            };
        }

        // GET: users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var res = await _userService.GetUser(id);

            return res.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.OK => Ok(res.ReturnObject),
                _ => NotFound(),
            };
        }

        // POST: users
        [HttpPost]
        public async Task<IActionResult> PostUser(UserRequestDTO userRequestDTO)
        {
            var res = await _userService.PostUser(userRequestDTO);

            return res.StatusCode switch
            {
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Created => CreatedAtAction("GetUser", new { id = res.ReturnObject.UserId }, res.ReturnObject),
                _ => NotFound(),
            };
        }

        // POST: users/:userId/games
        [HttpPost("{userId}/games")]
        public async Task<IActionResult> PostUserGame(int userId, GameRequestDTO gameRequestDTO)
        {
            var res = await _userService.PostUserGame(userId, gameRequestDTO);

            return res.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(),
                HttpStatusCode.NoContent => NoContent(),
                _ => NotFound(),
            };
        }

        // POST: users/:userId/comparison
        [HttpPost("{userId}/comparison")]
        public async Task<IActionResult> PostUserGameComparison(int userId, UserGameComparisonRequestDTO userGameComparisonRequestDTO)
        {
            var res = await _userService.GetUserGameComparison(userId, userGameComparisonRequestDTO);

            return res.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(),
                HttpStatusCode.OK => Ok(res.ReturnObject),
                _ => NotFound(),
            };
        }

        // DELETE: users/:userId/games/:gameId
        [HttpDelete("{userId}/games/{gameId}")]
        public async Task<IActionResult> DeleteUserGame(int userId, int gameId)
        {
            var res = await _userService.DeleteUserGame(userId, gameId);

            return res.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                HttpStatusCode.NoContent => NoContent(),
                _ => NotFound(),
            };
        }
    }
}
