namespace GamerAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
