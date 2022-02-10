using GamerAPI.Models;

namespace GamerAPI.Services
{
    public class UserService : IUserService
    {
        public Task<HttpResponseMessage> DeleteUserGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserGameComparisonDTO> GetUserGameComparison(int userId, int otherUserId, string comparison)
        {
            throw new NotImplementedException();
        }

        public Task<User> PostUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostUserGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
