﻿using GamerAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Game> GetGame(int gameId)
        {
            var url = BuildUrl($"games/{gameId}");

            var res = await _httpClient.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<Game>();
            }

            return null;
        }

        // TODO: Return a 400 Bad Request response if the q query parameter is missing, if the q query parameter is empty, or if the sort query parameter is invalid.
        // TODO: The RAWG API returns lots of game metadata. Your responses should only include the JSON properties shown in the example.
        public async Task<GameList> GetGames(string q, string sort = "")
        {
            var url = BuildUrl("games", $"&search={q}");

            // TODO: validate the sort - Any value supported by the RAWG API method should be supported here (e.g. name, -name).
            if (!string.IsNullOrEmpty(sort))
            {
                url += $"&ordering={sort}";
            }

            return await _httpClient.GetFromJsonAsync<GameList>(url);
        }

        private string BuildUrl(string endpoint, string q = "")
        {
            return string.Format($"/api/{endpoint}?key={_apiKey}");
        }
    }
}
