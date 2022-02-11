using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<GameListResponse> GetGames(string query, string sort);
    }
}
