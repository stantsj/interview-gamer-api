using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
        Task<User> PostUser(User user);
        Task<HttpResponseMessage> PostUserGame(int gameId);
        Task<HttpResponseMessage> DeleteUserGame(int gameId);
        Task<UserGameComparisonDTO> GetUserGameComparison(int userId, int otherUserId, string comparison);
    }
}
