using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<ServiceResult<GameList>> GetGames(string query, string sort);
        Task<ServiceResult<Game>> GetGame(int gameId);
    }
}
