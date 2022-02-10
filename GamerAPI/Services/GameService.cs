using GamerAPI.Models;

namespace GamerAPI.Services
{
    public class GameService : IGameService
    {
        Task<IEnumerable<Game>> GetGames(string q, string sort)
        {
            throw new NotImplementedException();
        }
    }
}
