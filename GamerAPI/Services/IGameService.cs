using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<GameListResponse> GetGames(string query, string sort);
        Task<Game> GetGame(int gameId);
    }
}
