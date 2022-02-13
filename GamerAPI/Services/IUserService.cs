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
        Task<HttpStatusCode> DeleteUserGame(int userId,int gameId);
        Task<ServiceResult<UserGameComparisonResponseDTO>> GetUserGameComparison(int userId, UserGameComparisonRequestDTO userGameComparisonRequestDTO);
    }
}
