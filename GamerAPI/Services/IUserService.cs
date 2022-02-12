using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamerAPI.Services
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<User> GetUser(int userId);
        Task<User> PostUser(User user);
        Task<HttpStatusCode> PostUserGame(int gameId, GameRequestDTO gameRequestDTO);
        Task<HttpResponseMessage> DeleteUserGame(int gameId);
        Task<UserGameComparisonDTO> GetUserGameComparison(int userId, int otherUserId, string comparison);
    }
}
