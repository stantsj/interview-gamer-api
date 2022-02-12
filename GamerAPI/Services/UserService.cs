using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GamerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private readonly IGameService _gameService;

        public UserService(UserContext context, IGameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public Task<HttpResponseMessage> DeleteUserGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<UserGameComparisonDTO> GetUserGameComparison(int userId, int otherUserId, string comparison)
        {
            throw new NotImplementedException();
        }

        public async Task<User> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<HttpStatusCode> PostUserGame(int userId, GameRequestDTO gameRequestDTO)
        {
            // Check to see if the user exists
            var user = await _context.Users.FindAsync(userId);
            
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }

            // Check to see if the game exists
            var game = await _gameService.GetGame(gameRequestDTO.GameId);

            if (game == null)
            {
                return HttpStatusCode.BadRequest;
            }

            // Check to see if the user already has the game
            try
            {
                user.Games.Add(game);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.Conflict;
            }
        }
    }
}
