using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<GameList> GetGames(string query, string sort);
        Task<Game> GetGame(int gameId);
    }
}
