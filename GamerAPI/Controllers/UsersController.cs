﻿#nullable disable
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

        // GET: users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        // GET: users/5
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

        // POST: users
        // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var response = await _userService.PostUser(user);
            return CreatedAtAction("GetUser", new { id = response.UserId }, response);
        }

        // POST: users/:userId/games
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

        // DELETE: users/:userId/games/:gameId
        [HttpDelete("{userId}/games/{gameId}")]
        public async Task<IActionResult> DeleteUserGame(int userId, int gameId)
        {
            var res = await _userService.DeleteUserGame(userId, gameId);

            switch (res)
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
