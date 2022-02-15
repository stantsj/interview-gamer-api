using GamerAPI.Models;

namespace GamerAPI.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMappingService _mappingService;

        private const string RAWG_API_KEY_CONFIG = "RAWG:ApiKey";
        private const string RAWG_BASE_URL_CONFIG = "RAWG:BaseUrl";

        private readonly string _apiKey;

        // TODO: use IHttpClientFactory
        public GameService(IConfiguration configuration, IMappingService mappingService)
        {
            _configuration = configuration;
            _mappingService = mappingService;
            _apiKey = _configuration[RAWG_API_KEY_CONFIG];

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_configuration[RAWG_BASE_URL_CONFIG])
            };
        }

        public async Task<ServiceResult<Game>> GetGame(int gameId)
        {
            var serviceResult = new ServiceResult<Game>();

            var url = BuildUrl($"games/{gameId}");
            var res = await _httpClient.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var game = await res.Content.ReadFromJsonAsync<Game>();

                serviceResult.StatusCode = ServiceStatusCode.Success;
                serviceResult.ReturnObject = game;
            }
            else
            {
                serviceResult.StatusCode = ServiceStatusCode.NotFound;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<GameResponseDTO>> GetGameResponseDTO(int gameId)
        {
            var serviceResult = new ServiceResult<GameResponseDTO>();

            var res = await GetGame(gameId);

            serviceResult.StatusCode = res.StatusCode;

            if (res.ReturnObject != null)
            {
                serviceResult.ReturnObject = _mappingService.GameToGameResponseDTO(res.ReturnObject);
            }

            return serviceResult;
        }

        // TODO: Return a 400 Bad Request response if the q query parameter is missing, if the q query parameter is empty, or if the sort query parameter is invalid.
        // TODO: The RAWG API returns lots of game metadata. Your responses should only include the JSON properties shown in the example.
        public async Task<ServiceResult<Games>> GetGames(string q, string sort = "")
        {
            var serviceResult = new ServiceResult<Games>();

            var url = BuildUrl("games", $"&search={q}");

            // TODO: validate the sort - Any value supported by the RAWG API method should be supported here (e.g. name, -name).
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&ordering={sort}";
            }

            var res = await _httpClient.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var gameList = await res.Content.ReadFromJsonAsync<Games>();
                serviceResult.StatusCode = ServiceStatusCode.Success;
                serviceResult.ReturnObject = gameList;
            }
            else
            {
                serviceResult.StatusCode = ServiceStatusCode.NotFound;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<GamesResponseDTO>> GetGamesResponseDTO(string q, string sort = "")
        {
            var serviceResult = new ServiceResult<GamesResponseDTO>();

            var res = await GetGames(q, sort);

            serviceResult.StatusCode = res.StatusCode;

            if (res.ReturnObject != null)
            {
                serviceResult.ReturnObject = _mappingService.GamesToGamesResponseDTO(res.ReturnObject);
            }

            return serviceResult;
        }

        private string BuildUrl(string endpoint, string q = "")
        {
            return string.Format($"/api/{endpoint}?key={_apiKey}");
        }
    }
}
