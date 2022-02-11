using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;

        public UserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


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
            //_dbContext.Users.Add(user);
            //var user = _dbContext.SaveChangesAsync();

            //return user;
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostUserGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
