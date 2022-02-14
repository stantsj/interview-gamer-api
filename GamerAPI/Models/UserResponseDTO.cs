namespace GamerAPI.Models
{
    public record UserResponseDTO
    {
        public int UserId { get; set; }
        public List<GameResponseDTO> Games { get; set; }
    }
}
