using GamerAPI.Models;

namespace GamerAPI.Services
{
    public interface IMappingService
    {
        public Game GameResponseDTOToGame(GameResponseDTO gameResponseDTO);
        public GameResponseDTO GameToGameResponseDTO(Game game);
        public GamesResponseDTO GamesToGamesResponseDTO(Games games);
        public UserResponseDTO UserToUserResponseDTO(User user);
        public List<GameResponseDTO> GamesListToGamesResponseDTOList(List<Game> games);
    }
}
