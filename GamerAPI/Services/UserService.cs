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

        // Get a user matching the specified userId.
        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserGameComparisonResponseDTO> GetUserGameComparison(int userId, int otherUserId, string comparison)
        {
            throw new NotImplementedException();
        }

        public async Task<User> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Add a game to the user's list of favorite games.
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
            catch
            {
                return HttpStatusCode.Conflict;
            }
        }

        // Remove a game from the user's list of favorite games.
        public async Task<HttpStatusCode> DeleteUserGame(int userId, int gameId)
        {
            var user = await _context.Users.FindAsync(userId);

            // Check to see if the user exists
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }

            // Remove a game from the user's list
            var game = user.Games.Find(x=> x.Id == gameId);

            // Return 404 if no game is found
            if (game == null)
            {
                return HttpStatusCode.NotFound;
            }

            try
            {
                user.Games.Remove(game);
                await _context.SaveChangesAsync();
                return HttpStatusCode.NoContent;
            }
            catch
            {
                return HttpStatusCode.Conflict;
            }
        }
    }
}
