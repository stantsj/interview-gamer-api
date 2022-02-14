using GamerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GamerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private readonly IGameService _gameService;
        private readonly IMappingService _mappingService;

        public UserService(UserContext context, IGameService gameService, IMappingService mappingService)
        {
            _context = context;
            _gameService = gameService;
            _mappingService = mappingService;
        }

        public async Task<ServiceResult<List<UserResponseDTO>>> GetUsers()
        {
            var serviceResult = new ServiceResult<List<UserResponseDTO>>();
            var list = new List<UserResponseDTO>();

            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            foreach(var user in users)
            {
                list.Add(_mappingService.UserToUserResponseDTO(user));
            }

            serviceResult.StatusCode = HttpStatusCode.OK;
            serviceResult.ReturnObject = list;

            return serviceResult;
        }

        // Get a user matching the specified userId.
        public async Task<ServiceResult<UserResponseDTO>> GetUser(int userId)
        {
            var serviceResult = new ServiceResult<UserResponseDTO>();

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            serviceResult.StatusCode = HttpStatusCode.OK;
            serviceResult.ReturnObject = _mappingService.UserToUserResponseDTO(user);

            return serviceResult;
        }

        public async Task<ServiceResult<UserGameComparisonResponseDTO>> GetUserGameComparison(int userId, UserGameComparisonRequestDTO request)
        {
            var serviceResult = new ServiceResult<UserGameComparisonResponseDTO>();

            // Return a 404 Not Found response if a user matching userId (in the URL) does not exist.
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            // Return a 400 Bad Request response if a user matching otherUserId (in the request body) does not exist
            // Return a 400 Bad Request if comparison (in the request body) is invalid.
            var otherUser = await _context.Users.FindAsync(request.OtherUserId);
            var comparison = request.Comparison;

            if (otherUser == null || !ValidateUserComparisonRequest(comparison))
            {
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                return serviceResult;
            }

            var returnObject = new UserGameComparisonResponseDTO();

            returnObject.UserId = userId;
            returnObject.OtherUserId = request.OtherUserId;
            returnObject.Comparison = comparison;

            switch (comparison)
            {
                case "difference":
                    var list1 = user.Games.Except(otherUser.Games).ToList();
                    var list2 = otherUser.Games.Except(user.Games).ToList();
                    var difference = list1.Concat(list2).ToList();
                    returnObject.Games = _mappingService.GamesListToGamesResponseDTOList(difference);
                    break;
                case "intersection":
                    var intersection = user.Games.Intersect(otherUser.Games).ToList();
                    returnObject.Games = _mappingService.GamesListToGamesResponseDTOList(intersection);
                    break;
                case "union":
                    var union = user.Games.Union(otherUser.Games).ToList();
                    returnObject.Games = _mappingService.GamesListToGamesResponseDTOList(union);
                    break;
            }

            serviceResult.StatusCode = HttpStatusCode.OK;
            serviceResult.ReturnObject = returnObject;

            return serviceResult;
        }

        public async Task<ServiceResult<UserResponseDTO>> PostUser(UserRequestDTO userRequestDTO)
        {
            var serviceResult = new ServiceResult<UserResponseDTO>();

            var user = new User()
            {
                UserId = userRequestDTO.UserId,
                Secret = "ADDSECRET"
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                serviceResult.StatusCode = HttpStatusCode.Created;
                serviceResult.ReturnObject = _mappingService.UserToUserResponseDTO(user);
            }
            catch (Exception ex)
            {
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
            }

            return serviceResult;
        }

        // Add a game to the user's list of favorite games.
        public async Task<ServiceResult<UserResponseDTO>> PostUserGame(int userId, GameRequestDTO gameRequestDTO)
        {
            var serviceResult = new ServiceResult<UserResponseDTO>();

            // Check to see if the user exists
            var user = await _context.Users.FindAsync(userId);
            
            if (user == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            // Check to see if the game exists
            var res = await _gameService.GetGame(gameRequestDTO.GameId);

            if (res.ReturnObject == null)
            {
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                return serviceResult;
            }

            // Check to see if the user already has the game
            try
            {
                var game = res.ReturnObject;
                user.Games.Add(game);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                serviceResult.StatusCode = HttpStatusCode.NoContent;
                return serviceResult;
            }
            catch
            {
                serviceResult.StatusCode = HttpStatusCode.Conflict;
                return serviceResult;
            }
        }

        // Remove a game from the user's list of favorite games.
        public async Task<ServiceResult<UserResponseDTO>> DeleteUserGame(int userId, int gameId)
        {
            var serviceResult = new ServiceResult<UserResponseDTO>();

            var user = await _context.Users.FindAsync(userId);

            // Check to see if the user exists
            if (user == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            // Remove a game from the user's list
            var game = user.Games.Find(x=> x.Id == gameId);

            // Return 404 if no game is found
            if (game == null)
            {
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                return serviceResult;
            }

            try
            {
                user.Games.Remove(game);
                await _context.SaveChangesAsync();
                serviceResult.StatusCode = HttpStatusCode.NoContent;
                return serviceResult;
            }
            catch
            {
                serviceResult.StatusCode = HttpStatusCode.Conflict;
                return serviceResult;
            }
        }

        private bool ValidateUserComparisonRequest(string comparison)
        {
            var comparisons = new [] {"union", "intersection", "difference"};
            return comparisons.Any(comparison.ToLower().Contains);
        }
    }
}
