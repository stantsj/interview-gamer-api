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
        private readonly UserContext _context;
        private readonly IUserService _userService;

        public UsersController(UserContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        // GET: Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: Users
        // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var res = await _userService.PostUser(user);
            return CreatedAtAction("GetUser", new { id = res.UserId }, res);
        }

        // POST: /users/:userId/games
        [HttpPost("{userId}/games")]
        public async Task<ActionResult<User>> PostUserGame(int userId, GameRequestDTO gameRequestDTO)
        {
            var res = await _userService.PostUserGame(userId, gameRequestDTO);

            switch(res)
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

        // DELETE: Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
