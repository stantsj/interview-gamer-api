namespace GamerAPI.Models
{
    public class GameListResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }    
        public IEnumerable<Game> Results { get; set;}
    }
}
