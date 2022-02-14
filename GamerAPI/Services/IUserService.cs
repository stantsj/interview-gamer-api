using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamerAPI.Services
{
    public interface IUserService
    {
        Task<ServiceResult<List<User>>> GetUsers();
        Task<ServiceResult<User>> GetUser(int userId);
        Task<ServiceResult<User>> PostUser(User user);
        Task<ServiceResult<User>> PostUserGame(int gameId, GameRequestDTO gameRequestDTO);
        Task<ServiceResult<User>> DeleteUserGame(int userId,int gameId);
        Task<ServiceResult<UserGameComparisonResponseDTO>> GetUserGameComparison(int userId, UserGameComparisonRequestDTO userGameComparisonRequestDTO);
    }
}
