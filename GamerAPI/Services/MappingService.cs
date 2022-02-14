﻿using GamerAPI.Models;

namespace GamerAPI.Services
{
    public class MappingService : IMappingService
    {
        public Game GameResponseDTOToGame(GameResponseDTO gameResponseDTO)
        {
            return new Game
            {
                Id = gameResponseDTO.GameId,
                Name = gameResponseDTO.Name,
                Added = gameResponseDTO.Added,
                Metacritic = gameResponseDTO.Metacritic,
                Rating = gameResponseDTO.Rating,
                Released = gameResponseDTO.Released,
                Updated = gameResponseDTO.Updated
            };
        }

        public GameResponseDTO GameToGameResponseDTO(Game game)
        {
            return new GameResponseDTO
            {
                GameId = game.Id,
                Name = game.Name,
                Added = game.Added,
                Metacritic = game.Metacritic,
                Rating = game.Rating,
                Released = game.Released,
                Updated = game.Updated
            };
        }
        public GamesResponseDTO GamesToGamesResponseDTO(Games games)
        {
            var gamesResponseDTO = new GamesResponseDTO();

            var results = games.Results.Select(g => new GameResponseDTO()
            {
                GameId = g.Id,
                Name = g.Name,
                Added = g.Added,
                Metacritic = g.Metacritic,
                Rating = g.Rating,
                Released = g.Released,
                Updated = g.Updated
            });

            gamesResponseDTO.Results = results;
            return gamesResponseDTO;
        }
    }
}
