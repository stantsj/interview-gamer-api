using System.ComponentModel.DataAnnotations;

namespace GamerAPI.Models
{
    public class GameRequestDTO
    {
        [Required]
        public int GameId { get; set; }
    }
}
