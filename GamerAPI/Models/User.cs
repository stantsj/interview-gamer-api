namespace GamerAPI.Models
{
    public record User
    {
        public int UserId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public string Secret { get; set; } = "DONOTSHOW";
    }
}
