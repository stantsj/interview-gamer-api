using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IMappingService
    {
        public Game GameResponseDTOToGame(GameResponseDTO gameResponseDTO);
        public GameResponseDTO GameToGameResponseDTO(Game game);
        public GamesResponseDTO GamesToGamesResponseDTO(Games games);
    }
}
