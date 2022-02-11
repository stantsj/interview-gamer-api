using GamerAPI.Models;
using System.Text.Json;

namespace GamerAPI.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private const string RAWG_API_KEY_CONFIG = "RAWG:ApiKey";
        private const string RAWG_BASE_URL_CONFIG = "RAWG:BaseUrl";

        private readonly string _apiKey;

        // TODO: use IHttpClientFactory
        public GameService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration[RAWG_API_KEY_CONFIG];

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_configuration[RAWG_BASE_URL_CONFIG])
            };
        }

        public async Task<GameListResponse> GetGames(string q, string sort = "")
        {
            var url = BuildUrl(q);

            // TODO: validate the sort
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&ordering={sort}";
            }

            return await _httpClient.GetFromJsonAsync<GameListResponse>(url);
        }

        private string BuildUrl(string q)
        {
            return string.Format($"/api/games?key={_apiKey}&search={q}");
        }
    }
}
