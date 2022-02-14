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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var res = await _userService.GetUsers();

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

        // GET: users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var res = await _userService.GetUser(id);

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

        // POST: users
        // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var res = await _userService.PostUser(user);

            switch (res.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Created:
                    return CreatedAtAction("GetUser", new { id = res.ReturnObject.UserId }, res.ReturnObject);
                default:
                    return NotFound();
            }
        }

        // POST: users/:userId/games
        [HttpPost("{userId}/games")]
        public async Task<IActionResult> PostUserGame(int userId, GameRequestDTO gameRequestDTO)
        {
            var res = await _userService.PostUserGame(userId, gameRequestDTO);

            switch(res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Conflict:
                    return Conflict();
                case HttpStatusCode.NoContent:
                    return NoContent();
                default:
                    return NotFound();
            }
        }

        // POST: users/:userId/comparison
        [HttpPost("{userId}/comparison")]
        public async Task<ActionResult<UserGameComparisonResponseDTO>> PostUserGameComparison(int userId, UserGameComparisonRequestDTO userGameComparisonRequestDTO)
        {
            var res = await _userService.GetUserGameComparison(userId, userGameComparisonRequestDTO);
            
            switch (res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Conflict:
                    return Conflict();
                case HttpStatusCode.OK:
                    return Ok(res.ReturnObject);
                default:
                    return NotFound();
            }
        }

        // DELETE: users/:userId/games/:gameId
        [HttpDelete("{userId}/games/{gameId}")]
        public async Task<IActionResult> DeleteUserGame(int userId, int gameId)
        {
            var res = await _userService.DeleteUserGame(userId, gameId);

            switch (res.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Conflict:
                    return Conflict();
                case HttpStatusCode.NoContent:
                    return NoContent();
                default:
                    return NotFound();
            }
        }
    }
}
