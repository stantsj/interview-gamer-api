﻿using System.ComponentModel.DataAnnotations;

namespace GamerAPI.Models
{
    public class GameRequestDTO
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Added { get; set; }
        public int? Metacritic { get; set; }
        public decimal Rating { get; set; }
        public string Released { get; set; }
        public string Updated { get; set; }
    }
}
