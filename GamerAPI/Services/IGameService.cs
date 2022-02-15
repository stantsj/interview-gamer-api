using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IGameService
    {
        Task<ServiceResult<Game>> GetGame(int gameId);
        Task<ServiceResult<Games>> GetGames(string query, string sort);
        Task<ServiceResult<GameResponseDTO>> GetGameResponseDTO(int gameId);
        Task<ServiceResult<GamesResponseDTO>> GetGamesResponseDTO(string query, string sort);
    }
}
