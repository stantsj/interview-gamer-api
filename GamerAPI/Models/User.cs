namespace GamerAPI.Models
{
    public class User
    {
        public string UserId { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
