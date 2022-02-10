namespace GamerAPI.Models
{
    public class UserGameComparisonDTO
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public string Comparison { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
