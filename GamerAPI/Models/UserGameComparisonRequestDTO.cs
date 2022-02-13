namespace GamerAPI.Models
{
    public class UserGameComparisonRequestDTO
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public string Comparison { get; set; }
    }
}
