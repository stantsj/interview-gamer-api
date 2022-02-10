using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGames(string q, string sort);
    }
}
