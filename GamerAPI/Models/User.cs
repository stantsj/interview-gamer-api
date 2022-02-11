namespace GamerAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
