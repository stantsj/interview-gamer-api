using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamerAPI.Services
{
    public interface IUserService
    {
        Task<ServiceResult<List<UserResponseDTO>>> GetUsers();
        Task<ServiceResult<UserResponseDTO>> GetUser(int userId);
        Task<ServiceResult<UserResponseDTO>> CreateUser(UserRequestDTO userRequestDTO);
        Task<ServiceResult<UserResponseDTO>> AddUserGame(int gameId, GameRequestDTO gameRequestDTO);
        Task<ServiceResult<UserResponseDTO>> DeleteUserGame(int userId,int gameId);
        Task<ServiceResult<UserGameComparisonResponseDTO>> GetUserGameComparison(int userId, UserGameComparisonRequestDTO userGameComparisonRequestDTO);
    }
}
